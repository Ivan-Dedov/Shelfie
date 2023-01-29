using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.AuthenticateUser;

public sealed record AuthenticateUserRequest
{
    [Required]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }
}
