using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Interactions.Models.ChangeStatus;

namespace Shelfie.Api.Interactions.Controllers;

[ApiController,
 Route("interactions/books")]
public sealed class BookController : Controller
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("update-status")]
    public async Task ChangeBookStatus(
        [Required, FromHeader] long userId,
        [Required, FromBody] ChangeBookStatusRequest request,
        CancellationToken ct)
    {
        
    }
}
