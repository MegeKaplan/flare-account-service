namespace Flare.AccountService.Services;

public interface IAccountService
{
    Task<string> CreateAccountAsync();
}