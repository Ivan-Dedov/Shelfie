using System.Data.Common;

namespace Shelfie.Api.Interactions.DataAccess.DataContexts;

public interface IShelfieApiInteractionsDataContext
{
    Task<DbConnection> GetConnection(
        CancellationToken cancellationToken
    );
}
