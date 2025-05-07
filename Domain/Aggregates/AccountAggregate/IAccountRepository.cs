using Domain.IRepository;

namespace Domain.Aggregates.AccountAggregate;

public interface IAccountRepository : IRepository<Account>
{
    Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken);
    Task<Account?> GetByUsername(string username, CancellationToken cancellationToken);
}
