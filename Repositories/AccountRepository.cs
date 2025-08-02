using Flare.AccountService.Models;
using Flare.AccountService.Data;

namespace Flare.AccountService.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FlareDbContext _db;

    public AccountRepository(FlareDbContext db)
    {
        _db = db;
    }

    public async Task<Account?> GetAccountByIdAsync(Guid userId)
    {
        return await _db.Accounts.FindAsync(userId);
    }
    
    public async Task<Account> CreateAccountAsync(Account account)
    {
        _db.Accounts.Add(account);
        await _db.SaveChangesAsync();
        return account;
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        _db.Accounts.Update(account);
        await _db.SaveChangesAsync();
        return account;
    }
}