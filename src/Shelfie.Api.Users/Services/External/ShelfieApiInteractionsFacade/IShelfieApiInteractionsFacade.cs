using Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade.Models;

namespace Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade;

public interface IShelfieApiInteractionsFacade
{
    Task<Statistics> GetStatisticsByUserId(
        long userId,
        CancellationToken cancellationToken
    );
}
