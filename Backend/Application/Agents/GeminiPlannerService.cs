using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Backend.DTOs.Agent;
using Microsoft.Extensions.Options;

namespace Backend.Application.Agents;

public class GeminiPlannerService : IAiPlannerService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiSettings _settings;
    private readonly ILogger<GeminiPlannerService> _logger;

    public GeminiPlannerService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiPlannerService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<AgentPlanDto> GeneratePlanAsync(
        string prompt,
        CancellationToken cancellationToken)
    {
        var systemPrompt = BuildPrompt(prompt);

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = systemPrompt
                        }
                    }
                }
            }
        };

        var endpoint =
            $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

        var response =
            await _httpClient.PostAsJsonAsync(
                endpoint,
                requestBody,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var rawResponse =
            await response.Content.ReadAsStringAsync(
                cancellationToken);

        _logger.LogInformation(
            "Gemini Response: {Response}",
            rawResponse);

        using var document =
            JsonDocument.Parse(rawResponse);

        var text =
            document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new Exception(
                "Gemini returned empty response.");
        }

        text = CleanJson(text);

        var plan =
            JsonSerializer.Deserialize<AgentPlanDto>(
                text,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (plan == null)
        {
            throw new Exception(
                "Failed to deserialize Gemini plan.");
        }

        NormalizePlan(plan);

        ValidatePlan(plan);

        return plan;
    }

    private static string CleanJson(string json)
    {
        json = json.Trim();

        if (json.StartsWith("```json"))
        {
            json = json.Replace("```json", "");
        }

        if (json.EndsWith("```"))
        {
            json = json.Replace("```", "");
        }

        return json.Trim();
    }

    private static string BuildPrompt(string userPrompt)
    {
        return $$"""
            You are an enterprise user-management planning agent.

            Return ONLY valid JSON.

            Never return markdown.
            Never return explanations.
            Never return code fences.

            SUPPORTED ACTIONS

            1. CreateUser
            2. AssignRole

            VALID REQUESTS

            Examples:

            - Create Rahul Sen
            - Create Rahul Sen as User
            - Create Rahul Sen as Admin
            - Create Rahul Sen as DemoAdmin
            - Create Rahul Sen as Admin with email rahul@gmail.com

            If the request is NOT clearly asking to create a user,
            return:

            {
              "steps": []
            }

            IMPORTANT RULES

            1. Extract information ONLY from the user's request.
            2. Never invent information.
            3. If Email is not provided, use "".
            4. If Password is not provided, use "".
            5. If PhoneNumber is not provided, use "".
            6. If LastName is not provided, use "".
            7. Username may be derived from the user's name.
            8. CreateUser must always be step 1.
            9. AssignRole may only appear after CreateUser.
            10. AssignRole is only allowed for Admin or DemoAdmin.
            11. For User role, do not generate AssignRole.
            12. AssignRole parameters must contain ONLY:
                {
                  "RoleName": ""
                }

            CreateUser schema:

            {
              "stepNumber": 1,
              "action": "CreateUser",
              "description": "",
              "parameters": {
                "FirstName": "",
                "LastName": "",
                "Email": "",
                "Username": "",
                "Password": "",
                "PhoneNumber": ""
              }
            }

            AssignRole schema:

            {
              "stepNumber": 2,
              "action": "AssignRole",
              "description": "",
              "parameters": {
                "RoleName": "Admin"
              }
            }

            Example Input:

            Create Rahul Sen as Admin

            Example Output:

            {
              "steps": [
                {
                  "stepNumber": 1,
                  "action": "CreateUser",
                  "description": "Create Rahul Sen user",
                  "parameters": {
                    "FirstName": "Rahul",
                    "LastName": "Sen",
                    "Email": "",
                    "Username": "rahul.sen",
                    "Password": "",
                    "PhoneNumber": ""
                  }
                },
                {
                  "stepNumber": 2,
                  "action": "AssignRole",
                  "description": "Assign Admin role",
                  "parameters": {
                    "RoleName": "Admin"
                  }
                }
              ]
            }

            User Request:

            {{userPrompt}}
            """;
    }

    private static void ValidatePlan(AgentPlanDto plan)
    {
        var allowedActions = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
    {
        AgentActions.CreateUser,
        AgentActions.AssignRole
    };

        if (plan.Steps.Count == 0)
        {
            return;
        }

        var firstStep = plan.Steps[0];

        if (!firstStep.Action.Equals(
                AgentActions.CreateUser,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                "The first step must be CreateUser.");
        }

        var createUserCount = plan.Steps.Count(x =>
        x.Action == AgentActions.CreateUser);

        if (createUserCount > 1)
        {
            throw new Exception("Only one CreateUser step is allowed.");
        }

        foreach (var step in plan.Steps)
        {
            if (!allowedActions.Contains(step.Action))
            {
                throw new Exception(
                    $"Invalid action '{step.Action}'.");
            }

            switch (step.Action)
            {
                case AgentActions.CreateUser:

                    ValidateCreateUserStep(step);

                    break;

                case AgentActions.AssignRole:

                    ValidateAssignRoleStep(step);

                    break;
            }
        }
    }

    private static void NormalizePlan(AgentPlanDto plan)
    {
        foreach (var step in plan.Steps)
        {
            if (step.Action == AgentActions.AssignRole)
            {
                if (step.Parameters.ContainsKey("Role") &&
                    !step.Parameters.ContainsKey("RoleName"))
                {
                    step.Parameters["RoleName"] =
                        step.Parameters["Role"];

                    step.Parameters.Remove("Role");
                }
            }
        }
    }
    private static void ValidateCreateUserStep(
    AgentStepDto step)
    {
        var requiredKeys = new[]
        {
        "FirstName",
        "LastName",
        "Email",
        "Username",
        "Password",
        "PhoneNumber"
    };

        foreach (var key in requiredKeys)
        {
            if (!step.Parameters.ContainsKey(key))
            {
                throw new Exception(
                    $"CreateUser missing parameter '{key}'.");
            }
        }
    }

    private static void ValidateAssignRoleStep(
    AgentStepDto step)
    {
        if (!step.Parameters.TryGetValue(
                "RoleName",
                out var roleName))
        {
            throw new Exception(
                "AssignRole missing RoleName.");
        }

        var validRoles = new[]
        {
        "Admin",
        "DemoAdmin"
    };

        if (!validRoles.Contains(
                roleName,
                StringComparer.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Invalid role '{roleName}'.");
        }
    }

}