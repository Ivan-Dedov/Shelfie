using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Shelves.Handlers.Shelves.GetCommonShelves;
using Shelfie.Api.Shelves.Handlers.Shelves.GetShelfContents;
using Shelfie.Api.Shelves.Models.GetCommonShelves;
using Shelfie.Api.Shelves.Models.GetShelfContents;

namespace Shelfie.Api.Shelves.Controllers;

[ApiController,
 Route("shelves/shelves")]
public sealed class ShelvesController : Controller
{
    private readonly IMediator _mediator;

    public ShelvesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<GetCommonShelvesResponse> GetCommonShelves(
        [Required, FromHeader] long userId,
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new GetCommonShelvesCommand(userId),
            ct);

        return response;
    }

    [HttpGet("{shelfId:long:min(1)}/contents")]
    public async Task<GetShelfContentsResponse> GetShelfContents(
        [Required, FromHeader] long userId,
        [Required, FromRoute] long shelfId,
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new GetShelfContentsCommand(
                userId,
                shelfId),
            ct);

        return response;
    }
}
