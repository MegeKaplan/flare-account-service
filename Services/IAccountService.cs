using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.DTOs;
using Flare.AccountService.Models;

namespace Flare.AccountService.Services;

public interface IAccountService
{
    Task<List<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(Guid targetUserId);
    Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid principalUserId, Guid targetUserId);
    Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid principalUserId, Guid targetUserId);
    Task DeleteAccountAsync(Guid principalUserId, Guid targetUserId, bool hard = false);
}