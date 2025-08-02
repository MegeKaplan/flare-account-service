using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.DTOs;

namespace Flare.AccountService.Services;

public interface IAccountService
{
    Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid userId);
    Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid userId);
    Task DeleteAccountAsync(Guid userId, bool hard = false);
}