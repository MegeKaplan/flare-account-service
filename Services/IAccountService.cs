using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.DTOs;
using Flare.AccountService.Models;
using Flare.AccountService.DTOs.Account;

namespace Flare.AccountService.Services;

public interface IAccountService
{
    Task<List<AccountPublicDto>> GetAllAccountsAsync();
    Task<AccountPublicDto?> GetAccountByIdAsync(Guid targetUserId);
    Task<AccountPublicDto?> GetAccountByUsernameAsync(string username);
    Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid principalUserId, Guid targetUserId);
    Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid principalUserId, Guid targetUserId);
    Task DeleteAccountAsync(Guid principalUserId, Guid targetUserId, bool hard = false);
}