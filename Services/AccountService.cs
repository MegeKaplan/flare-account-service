using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

using Flare.AccountService.Repositories;
using Flare.AccountService.DTOs;
using Flare.AccountService.Models;
using Flare.AccountService.Utils;

namespace Flare.AccountService.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return await _accountRepository.GetAllAccountsAsync();
    }

    public async Task<Account?> GetAccountByIdAsync(Guid userId)
    {
        return await _accountRepository.GetAccountByIdAsync(userId);
    }

    public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid userId)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var account = new Account
        {
            Id = userId,
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
        };

        var createdAccount = await _accountRepository.CreateAccountAsync(account);

        var response = new CreateAccountResponse
        {
            Id = createdAccount.Id,
            Email = createdAccount.Email,
            Username = createdAccount.Username,
            CreatedAt = createdAccount.CreatedAt,
        };

        return response;
    }

    public async Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid principalUserId, Guid targetUserId)
    {
        if (!AuthorizationHelper.IsOwner(principalUserId, targetUserId)) throw new UnauthorizedAccessException("You cannot update another user's account");

        var account = await _accountRepository.GetAccountByIdAsync(targetUserId) ?? throw new Exception("Account not found");

        account.Email = request.Email ?? account.Email;
        account.Username = request.Username ?? account.Username;

        if (!string.IsNullOrEmpty(request.Password))
        {
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        var updatedAccount = await _accountRepository.UpdateAccountAsync(account);

        return new UpdateAccountResponse
        {
            Id = updatedAccount.Id,
            Email = updatedAccount.Email,
            Username = updatedAccount.Username,
            CreatedAt = updatedAccount.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public async Task DeleteAccountAsync(Guid principalUserId, Guid targetUserId, bool hard = false)
    {
        if (!AuthorizationHelper.IsOwner(principalUserId, targetUserId)) throw new UnauthorizedAccessException("You cannot delete another user's account");

        var account = await _accountRepository.GetAccountByIdAsync(targetUserId) ?? throw new Exception("Account not found");

        if (hard)
        {
            await _accountRepository.DeleteAccountAsync(account);
            return;
        }

        account.DeletedAt = DateTime.UtcNow;

        await _accountRepository.UpdateAccountAsync(account);
    }
}