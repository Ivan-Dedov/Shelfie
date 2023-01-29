using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.AuthenticateUser;

public sealed record AuthenticateUserResponse
{
    [Required]
    public string AccessToken { get; init; }
    
    [Required]
    public string RefreshToken { get; init; }
}
