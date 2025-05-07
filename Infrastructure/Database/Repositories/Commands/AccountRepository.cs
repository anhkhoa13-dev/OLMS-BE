using Domain.Aggregates.AccountAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Commands;

public class AccountRepository(ApplicationDbContext context) : Repository<Account>(context), IAccountRepository
{
    public async Task<Account?> GetByUsername(string username, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .SingleOrDefaultAsync(a => a.Username == username, cancellationToken);
    }

    public async Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken)
    {
        return await _context.Accounts.AnyAsync(a => a.Username == username, cancellationToken);
    }
}
