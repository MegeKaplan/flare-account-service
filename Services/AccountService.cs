using Flare.AccountService.Repositories;

namespace Flare.AccountService.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<string> CreateAccountAsync()
    {
        var result = await _accountRepository.CreateAccountAsync();
        return result;
    }

}