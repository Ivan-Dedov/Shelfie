using MediatR;
using Shelfie.Api.Books.Models.GetFilters;

namespace Shelfie.Api.Books.Handlers.Search.GetFilters;

public sealed record GetFiltersCommand : IRequest<GetFiltersResponse>;

public sealed class GetFiltersCommandHandler : IRequestHandler<GetFiltersCommand, GetFiltersResponse>
{
    public async Task<GetFiltersResponse> Handle(
        GetFiltersCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
