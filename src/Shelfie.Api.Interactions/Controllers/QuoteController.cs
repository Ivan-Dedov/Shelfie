using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Interactions.Models.AddQuoteRequest;

namespace Shelfie.Api.Interactions.Controllers;

[ApiController,
 Route("interactions/quotes")]
public sealed class QuoteController : Controller
{
    private readonly IMediator _mediator;

    public QuoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task AddQuote(
        [Required, FromHeader] long userId,
        [Required, FromBody] AddQuoteRequest request,
        CancellationToken ct)
    {
        
    }
}
