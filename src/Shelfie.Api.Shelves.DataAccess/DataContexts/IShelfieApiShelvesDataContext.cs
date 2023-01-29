using System.Data.Common;

namespace Shelfie.Api.Shelves.DataAccess.DataContexts;

public interface IShelfieApiShelvesDataContext
{
    Task<DbConnection> GetConnection(
        CancellationToken cancellationToken
    );
}
