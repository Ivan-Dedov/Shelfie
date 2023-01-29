using MediatR;
using Shelfie.Api.Shelves.Models.GetCommonShelves;

namespace Shelfie.Api.Shelves.Handlers.Shelves.GetCommonShelves;

public sealed record GetCommonShelvesCommand(
    long UserId
) : IRequest<GetCommonShelvesResponse>;

public sealed class GetCommonShelvesCommandHandler : IRequestHandler<GetCommonShelvesCommand, GetCommonShelvesResponse>
{
    public async Task<GetCommonShelvesResponse> Handle(
        GetCommonShelvesCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
