using Flare.AccountService.Models;

namespace Flare.AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account> CreateAccountAsync(Account account);
}