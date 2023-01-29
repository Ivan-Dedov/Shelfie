namespace Shelfie.Api.Users.Services.JwtService.Models;

public struct TokenPair
{
    public string AccessToken { get; init; }

    public string RefreshToken { get; init; }
}
