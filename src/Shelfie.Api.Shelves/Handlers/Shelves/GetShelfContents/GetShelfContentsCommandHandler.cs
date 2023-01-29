using MediatR;
using Shelfie.Api.Shelves.Models.GetShelfContents;

namespace Shelfie.Api.Shelves.Handlers.Shelves.GetShelfContents;

public sealed record GetShelfContentsCommand(
    long UserId,
    long ShelfId
) : IRequest<GetShelfContentsResponse>;

public sealed class GetShelfContentsCommandHandler : IRequestHandler<GetShelfContentsCommand, GetShelfContentsResponse>
{
    public async Task<GetShelfContentsResponse> Handle(
        GetShelfContentsCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
