using Shelfie.Libs.Common.Models;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository.Models;

public sealed record UserBook
{
    public long BookId { get; }

    public BookStatus BookStatus { get; }

    public UserBook(UserBookDao dao)
    {
        BookId = dao.BookId;
        BookStatus = Enum.TryParse<BookStatus>(dao.BookStatus, out var status)
            ? status
            : BookStatus.Unknown;
    }
}
