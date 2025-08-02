using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.DTOs;
using Flare.AccountService.Models;

namespace Flare.AccountService.Services;

public interface IAccountService
{
    Task<List<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(Guid userId);
    Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid userId);
    Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid userId);
    Task DeleteAccountAsync(Guid userId, bool hard = false);
}