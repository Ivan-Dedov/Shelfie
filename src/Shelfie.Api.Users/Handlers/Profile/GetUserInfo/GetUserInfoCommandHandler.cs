using MediatR;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository;
using Shelfie.Api.Users.Models.GetUserInfo;
using Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade;

namespace Shelfie.Api.Users.Handlers.Profile.GetUserInfo;

public sealed record GetUserInfoCommand(
    long UserId
) : IRequest<GetUserInfoResponse>;

public sealed class GetUserInfoCommandHandler : IRequestHandler<GetUserInfoCommand, GetUserInfoResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IShelfieApiInteractionsFacade _shelfieApiInteractionsFacade;

    public GetUserInfoCommandHandler(
        IUserRepository userRepository,
        IShelfieApiInteractionsFacade shelfieApiInteractionsFacade
    )
    {
        _userRepository = userRepository;
        _shelfieApiInteractionsFacade = shelfieApiInteractionsFacade;
    }

    public async Task<GetUserInfoResponse> Handle(
        GetUserInfoCommand command,
        CancellationToken ct)
    {
        var user = await GetUserById(command.UserId, ct);
        var statistics = await _shelfieApiInteractionsFacade.GetStatisticsByUserId(command.UserId, ct);

        return new GetUserInfoResponse
        {
            Name = user.Name,
            Email = user.Email,
            BannerImageUrl = user.BannerImageUrl,
            ProfileImageUrl = user.ProfileImageUrl,
            Statistics = new UserStatistics
            {
                BookCount = statistics.BookCount,
                QuoteCount = statistics.QuoteCount,
                ReviewCount = statistics.ReviewCount
            }
        };
    }

    private async Task<DataAccess.Repositories.UserRepository.Models.User> GetUserById(
        long userId,
        CancellationToken ct)
    {
        var user = await _userRepository.GetUserById(userId, ct);
        if (user is null)
        {
            throw new ApplicationException("Пользователь не найден");
        }

        return user;
    }
}
