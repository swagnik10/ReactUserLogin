using Backend.Application.AI.Gemini;
using Backend.DTOs.Agent;
using Backend.DTOs.AI.Ai_Fun;
using Microsoft.Extensions.Options;

namespace Backend.Application.AI.AiFun;

public class GeminiUserFunService
    : GeminiBaseService, IAiUserFunService
{
    public GeminiUserFunService(
        HttpClient httpClient,
        IOptions<GeminiSettings> settings,
        ILogger<GeminiUserFunService> logger)
        : base(httpClient, settings, logger)
    {
    }


    public Task<NicknameResponse> GenerateNicknameAsync(
     UserFunRequest request,
     CancellationToken cancellationToken)
    {
        return GenerateAsync<NicknameResponse>(
            BuildNicknamePrompt(request),
            NicknameSchema,
            "Nickname Generation",
            cancellationToken);
    }

    public Task<RoastResponse> GenerateRoastAsync(
     UserFunRequest request,
     CancellationToken cancellationToken)
    {
        return GenerateAsync<RoastResponse>(
            BuildRoastPrompt(request),
            RoastSchema,
            "Roast Generation",
            cancellationToken);
    }

    public Task<FortuneResponse> GenerateFortuneAsync(
    UserFunRequest request,
    CancellationToken cancellationToken)
    {
        return GenerateAsync<FortuneResponse>(
            BuildFortunePrompt(request),
            FortuneSchema,
            "Fortune Generation",
            cancellationToken);
    }

    public Task<AchievementResponse> GenerateAchievementAsync(
    UserFunRequest request,
    CancellationToken cancellationToken)
    {
        return GenerateAsync<AchievementResponse>(
            BuildAchievementPrompt(request),
            AchievementSchema,
            "Achievement Generation",
            cancellationToken);
    }

    private static string BuildNicknamePrompt(UserFunRequest request)
    {
        return $$"""
            You are a fun and creative assistant.

            Your task is to generate a developer-style nickname for the user.

            User Information:
            - First Name: {{request.FirstName}}
            - Last Name: {{request.LastName}}
            - Username: {{request.Username}}
            - Role: {{request.Role}}

            Rules:
            - Generate exactly one unique developer-style nickname.
            - Include exactly one emoji that matches the nickname.
            - Explain the nickname in exactly one short sentence.
            - Keep the tone friendly, lighthearted, and humorous.
            - Do not insult, mock, or offend the user.
            - Do not mention AI, prompts, or that you generated the nickname.
            - Do not include markdown.
            - Do not include code fences.
            - Do not include any text before or after the JSON.

            Return ONLY valid JSON in the following format:

            {
              "nickname": "",
              "emoji": "",
              "reason": ""
            }
            """;
    }

    private static string BuildRoastPrompt(UserFunRequest request)
    {
        return $$"""
        You are a friendly comedian.

        Roast this user in a playful and light-hearted manner.

        User Information:
        - First Name: {request.FirstName}
        - Username: {request.Username}
        - Role: {request.Role}

        Rules:
        - Maximum 30 words.
        - Keep it funny.
        - Never be offensive.
        - Never mention race, religion, politics, gender, appearance or disabilities.
        - Never use profanity.
        - The roast should make the user smile.

        Return ONLY valid JSON.

        JSON format:
        {
    
              "roast": ""
        }
        """;
    }

    private static string BuildFortunePrompt(UserFunRequest request)
    {
        return $$"""
        You are generating a funny developer fortune cookie.

        User Information:
        - First Name: {request.FirstName}
        - Username: {request.Username}
        - Role: {request.Role}

        Rules:
        - Write exactly one sentence.
        - Make it positive.
        - Make it developer or technology themed.
        - It should sound like a fortune cookie.
        - Avoid negativity.

        Return ONLY valid JSON.

        JSON format:
        {
    
              "fortune": ""
        }
        """;
    }

    private static string BuildAchievementPrompt(UserFunRequest request)
    {
        return $$"""
        You are designing a fun video game achievement for a user.

        User Information:
        - First Name: {request.FirstName}
        - Username: {request.Username}
        - Role: {request.Role}

        Rules:
        - Generate one achievement title.
        - Include one matching emoji.
        - Write one short description.
        - Make it feel like a game achievement.
        - Keep it positive and fun.
        - Do not mention AI.

        Return ONLY valid JSON.

        JSON format:
        {
    
          "title": "",
          "emoji": "",
          "description": ""
        }
        """;
    }

    private static readonly object NicknameSchema = new
    {
        type = "OBJECT",
        properties = new
        {
            nickname = new { type = "STRING" },
            emoji = new { type = "STRING" },
            reason = new { type = "STRING" }
        },
        required = new[] { "nickname", "emoji", "reason" }
    };

    private static readonly object RoastSchema = new
    {
        type = "OBJECT",
        properties = new
        {
            roast = new { type = "STRING" }
        },
        required = new[] { "roast" }
    };

    private static readonly object FortuneSchema = new
    {
        type = "OBJECT",
        properties = new
        {
            fortune = new { type = "STRING" }
        },
        required = new[] { "fortune" }
    };

    private static readonly object AchievementSchema = new
    {
        type = "OBJECT",
        properties = new
        {
            title = new { type = "STRING" },
            emoji = new { type = "STRING" },
            description = new { type = "STRING" }
        },
        required = new[] { "title", "emoji", "description" }
    };


}
