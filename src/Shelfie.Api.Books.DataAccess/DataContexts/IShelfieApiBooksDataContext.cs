using System.Data.Common;

namespace Shelfie.Api.Books.DataAccess.DataContexts;

public interface IShelfieApiBooksDataContext
{
    Task<DbConnection> GetConnection(
        CancellationToken cancellationToken
    );
}
