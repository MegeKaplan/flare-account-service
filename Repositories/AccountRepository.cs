namespace Flare.AccountService.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task<string> CreateAccountAsync()
    {
        return Task.FromResult("account created");
    }
}