using MediatR;
using Shelfie.Api.Books.Models.GetBook;

namespace Shelfie.Api.Books.Handlers.Search.GetBooksCommandHandler;

public sealed class GetBooksCommandHandler : IRequestHandler<GetBooksCommand, IReadOnlyCollection<GetBookResponse>>
{
    public async Task<IReadOnlyCollection<GetBookResponse>> Handle(
        GetBooksCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
