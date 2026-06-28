using Backend.DTOs.AI.Ai_Fun;

namespace Backend.Application.AI.AiFun;

public interface IAiUserFunService
{
    Task<NicknameResponse> GenerateNicknameAsync(
    UserFunRequest request,
    CancellationToken cancellationToken);

    Task<RoastResponse> GenerateRoastAsync(
        UserFunRequest request,
        CancellationToken cancellationToken);

    Task<FortuneResponse> GenerateFortuneAsync(
        UserFunRequest request,
        CancellationToken cancellationToken);

    Task<AchievementResponse> GenerateAchievementAsync(
        UserFunRequest request,
        CancellationToken cancellationToken);
}
