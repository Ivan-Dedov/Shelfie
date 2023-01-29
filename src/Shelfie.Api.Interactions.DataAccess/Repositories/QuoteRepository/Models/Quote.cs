namespace Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository.Models;

public sealed record Quote
{
    public long Id { get; }

    public long BookId { get; }

    public long UserId { get; }

    public string Text { get; }

    public DateTime CreatedAt { get; }

    public Quote(QuoteDao dao)
    {
        Id = dao.Id;
        BookId = dao.BookId;
        UserId = dao.UserId;
        Text = dao.Text;
        CreatedAt = dao.CreatedAt;
    }
}
