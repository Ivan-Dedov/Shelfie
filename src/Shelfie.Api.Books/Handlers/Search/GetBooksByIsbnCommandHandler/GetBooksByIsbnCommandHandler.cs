using MediatR;
using Shelfie.Api.Books.Models.GetBook;

namespace Shelfie.Api.Books.Handlers.Search.GetBooksByIsbnCommandHandler;

public sealed record GetBooksByIsbnCommand(
    long UserId,
    string Isbn
) : IRequest<GetBookResponse>;

public sealed class GetBooksByIsbnCommandHandler : IRequestHandler<GetBooksByIsbnCommand, GetBookResponse>
{
    public async Task<GetBookResponse> Handle(
        GetBooksByIsbnCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
