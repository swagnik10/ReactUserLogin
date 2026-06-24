using System.Text.RegularExpressions;
using Backend.Application.Agents;
using Backend.Application.CommandAndQuery;
using Backend.DTOs.Agent;
using MediatR;

namespace Backend.Application.Handlers.AgentHandler;

public class GeneratePlanHandler
    : IRequestHandler<GeneratePlanRequest, AgentPlanDto>
{
    public Task<AgentPlanDto> Handle(
        GeneratePlanRequest request,
        CancellationToken cancellationToken)
    {
        var prompt = request.Prompt.Trim();

        var firstName = ExtractName(prompt);

        var role = ExtractRole(prompt);

        var username = firstName.ToLower();

        var plan = new AgentPlanDto();

        plan.Steps.Add(
            new AgentStepDto
            {
                StepNumber = 1,
                Action = AgentActions.CreateUser,
                Description = $"Create {firstName} user",
                Parameters = new Dictionary<string, string>
                {
                    ["FirstName"] = firstName,
                    ["LastName"] = "",
                    ["Email"] = "",
                    ["Username"] = username,
                    ["Password"] = "",
                    ["PhoneNumber"] = ""
                }
            });

        if (!role.Equals("User",
                StringComparison.OrdinalIgnoreCase))
        {
            plan.Steps.Add(
                new AgentStepDto
                {
                    StepNumber = 2,
                    Action = AgentActions.AssignRole,
                    Description = $"Assign {role} role",
                    Parameters = new Dictionary<string, string>
                    {
                        ["RoleName"] = role
                    }
                });
        }

        return Task.FromResult(plan);
    }

    private static string ExtractName(string prompt)
    {
        var match = Regex.Match(
            prompt,
            @"create\s+([a-zA-Z]+)",
            RegexOptions.IgnoreCase);

        return match.Success
            ? match.Groups[1].Value
            : "Employee";
    }

    private static string ExtractRole(string prompt)
    {
        if (prompt.Contains(
                "demoadmin",
                StringComparison.OrdinalIgnoreCase))
        {
            return "DemoAdmin";
        }

        if (prompt.Contains(
                "admin",
                StringComparison.OrdinalIgnoreCase))
        {
            return "Admin";
        }

        return "User";
    }
}