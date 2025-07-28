namespace Flare.AccountService.Repositories;

public interface IAccountRepository
{
    Task<string> CreateAccountAsync();
}