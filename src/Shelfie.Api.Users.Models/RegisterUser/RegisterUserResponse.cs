using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.RegisterUser;

public sealed record RegisterUserResponse
{
    [Required]
    public string AccessToken { get; init; }
    
    [Required]
    public string RefreshToken { get; init; }
}
