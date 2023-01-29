using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Interactions.Models.AddReviewRequest;

namespace Shelfie.Api.Interactions.Controllers;

[ApiController,
 Route("interactions/reviews")]
public sealed class ReviewController : Controller
{
    private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task AddReview(
        [Required, FromHeader] long userId,
        [Required, FromBody] AddReviewRequest request,
        CancellationToken ct)
    {
        
    }
}
