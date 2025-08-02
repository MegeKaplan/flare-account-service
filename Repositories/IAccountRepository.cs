using Flare.AccountService.Models;

namespace Flare.AccountService.Repositories;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(Guid userId);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(Account account);
}