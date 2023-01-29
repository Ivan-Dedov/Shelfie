using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.RegisterUser;

public sealed record RegisterUserRequest
{
    [Required]
    public string Username { get; init; }

    [Required]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }
}
