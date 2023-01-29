using System.Data.Common;

namespace Shelfie.Api.Users.DataAccess.DataContexts;

public interface IShelfieApiUsersDataContext
{
    Task<DbConnection> GetConnection(
        CancellationToken cancellationToken);
}
