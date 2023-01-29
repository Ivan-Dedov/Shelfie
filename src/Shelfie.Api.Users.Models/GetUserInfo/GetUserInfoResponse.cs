using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.GetUserInfo;

public sealed record GetUserInfoResponse
{
    [Required]
    public string Name { get; init; }

    [Required]
    public string Email { get; init; }

    public string? ProfileImageUrl { get; init; }

    public string? BannerImageUrl { get; init; }

    [Required]
    public UserStatistics Statistics { get; init; }
}
