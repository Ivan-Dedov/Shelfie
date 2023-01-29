using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Users.Handlers.Profile.GetUserInfo;
using Shelfie.Api.Users.Models.GetUserInfo;

namespace Shelfie.Api.Users.Controllers;

[ApiController,
 Route("users/profile")]
public sealed class ProfileController : Controller
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves the information to display in the user's profile.
    /// </summary>
    /// <param name="userId">The ID of a user to get information about.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Information for the user's profile: name, e-mail, profile image, short statistical summary, etc.</returns>
    [HttpGet]
    public async Task<GetUserInfoResponse> GetUserInfo(
        [Required, FromHeader] long userId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetUserInfoCommand(
                userId
            ),
            ct);

        return result;
    }
}
