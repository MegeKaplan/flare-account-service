using Flare.AccountService.Models;

namespace Flare.AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountByIdAsync(Guid userId);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
}