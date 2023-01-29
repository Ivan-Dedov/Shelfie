using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Users.Handlers.User.AuthenticateUser;
using Shelfie.Api.Users.Handlers.User.RefreshToken;
using Shelfie.Api.Users.Handlers.User.RegisterUser;
using Shelfie.Api.Users.Handlers.User.RevokeToken;
using Shelfie.Api.Users.Models.AuthenticateUser;
using Shelfie.Api.Users.Models.RefreshToken;
using Shelfie.Api.Users.Models.RegisterUser;
using Shelfie.Api.Users.Models.RevokeToken;

namespace Shelfie.Api.Users.Controllers;

[ApiController,
 Route("users/user")]
public sealed class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticates the user in the Shelfie application.
    /// </summary>
    /// <param name="request">The request containing the user's e-mail and password.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The ID of the authenticated user, as well as the access and refresh tokens.</returns>
    [AllowAnonymous,
     HttpPost("login")]
    public async Task<AuthenticateUserResponse> AuthenticateUser(
        [Required, FromBody] AuthenticateUserRequest request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new AuthenticateUserCommand(
                request.Email,
                request.Password
            ),
            ct);

        return new AuthenticateUserResponse
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">The request containing the user's e-mail and password.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The ID of the authenticated user, as well as the access and refresh tokens.</returns>
    [AllowAnonymous,
     HttpPost("register")]
    public async Task<RegisterUserResponse> RegisterUser(
        [Required, FromBody] RegisterUserRequest request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new RegisterUserCommand(
                request.Username,
                request.Email,
                request.Password
            ),
            ct);

        return new RegisterUserResponse
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }

    /// <summary>
    /// Refreshes the access token using the provided refresh token.
    /// </summary>
    /// <param name="request">The request containing the refresh token.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The new access and refresh tokens.</returns>
    [AllowAnonymous,
     HttpPost("refresh-token")]
    public async Task<RefreshTokenResponse> RefreshToken(
        [Required, FromBody] RefreshTokenRequest request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new RefreshTokenCommand(
                request.RefreshToken
            ),
            ct);

        return new RefreshTokenResponse
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }

    /// <summary>
    /// Revokes the refresh token.
    /// </summary>
    /// <param name="request">The refresh token to be revoked.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <remarks>Please note that this action does not revoke the access token, only the provided refresh token.</remarks>
    [HttpPost("revoke-token")]
    public Task RevokeToken(
        [Required, FromBody] RevokeTokenRequest request,
        CancellationToken ct)
    {
        return _mediator.Send(
            new RevokeTokenCommand(
                request.RefreshToken
            ),
            ct);
    }
}
