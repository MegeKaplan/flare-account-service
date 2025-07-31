using Flare.AccountService.Models;

namespace Flare.AccountService.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task<Account> CreateAccountAsync(Account account)
    {
        // database operations
        var createdAccount = account;

        return Task.FromResult(createdAccount);
    }
}