using Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Models;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Parameters;

namespace Shelfie.Api.Users.DataAccess.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User?> GetUserById(
        long userId,
        CancellationToken cancellationToken
    );

    Task<User?> GetUserByEmail(
        string email,
        CancellationToken cancellationToken
    );

    Task<User> CreateUser(
        CreateUserParameters parameters,
        CancellationToken cancellationToken
    );
}
