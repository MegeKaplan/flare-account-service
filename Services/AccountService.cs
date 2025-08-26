using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

using Flare.AccountService.Repositories;
using Flare.AccountService.DTOs;
using Flare.AccountService.Models;
using Flare.AccountService.Utils;
using Flare.AccountService.DTOs.Account;

namespace Flare.AccountService.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<AccountPublicDto>> GetAllAccountsAsync()
    {
        var accounts = await _accountRepository.GetAllAccountsAsync();

        return accounts.Select(account => new AccountPublicDto
        {
            Id = account.Id,
            Username = account.Username,
            DisplayName = account.DisplayName,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        }).ToList();
    }

    public async Task<AccountPublicDto?> GetAccountByIdAsync(Guid targetUserId)
    {
        var account = await _accountRepository.GetAccountByIdAsync(targetUserId);
        if (account == null) return null;

        return new AccountPublicDto
        {
            Id = account.Id,
            Username = account.Username,
            DisplayName = account.DisplayName,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }

    public async Task<AccountPublicDto?> GetAccountByUsernameAsync(string username)
    {
        var account = await _accountRepository.GetAccountByUsernameAsync(username);
        if (account == null) return null;

        return new AccountPublicDto
        {
            Id = account.Id,
            Username = account.Username,
            DisplayName = account.DisplayName,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }

    public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, Guid principalUserId, Guid targetUserId)
    {
        if (!AuthorizationHelper.IsOwner(principalUserId, targetUserId)) throw new UnauthorizedAccessException("You cannot create an account for another user");

        if (await _accountRepository.GetAccountByIdAsync(targetUserId) != null) throw new Exception("Account already exists");

        var existingAccountByUsername = await _accountRepository.GetAccountByUsernameAsync(request.Username);
        if (existingAccountByUsername != null) throw new Exception("Username already taken");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var account = new Account
        {
            Id = targetUserId,
            Email = request.Email,
            Username = request.Username,
            DisplayName = request.DisplayName,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
        };

        var createdAccount = await _accountRepository.CreateAccountAsync(account);

        return new CreateAccountResponse
        {
            Id = createdAccount.Id,
            Email = createdAccount.Email,
            Username = createdAccount.Username,
            DisplayName = createdAccount.DisplayName,
            CreatedAt = createdAccount.CreatedAt,
        };
    }

    public async Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request, Guid principalUserId, Guid targetUserId)
    {
        if (!AuthorizationHelper.IsOwner(principalUserId, targetUserId)) throw new UnauthorizedAccessException("You cannot update another user's account");

        var account = await _accountRepository.GetAccountByIdAsync(targetUserId) ?? throw new Exception("Account not found");

        if (request.Username != null && request.Username != account.Username)
        {
            var existingAccountByUsername = await _accountRepository.GetAccountByUsernameAsync(request.Username);
            if (existingAccountByUsername != null) throw new Exception("Username already taken");
        }

        account.Email = request.Email ?? account.Email;
        account.Username = request.Username ?? account.Username;
        account.DisplayName = request.DisplayName ?? account.DisplayName;

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
            DisplayName = updatedAccount.DisplayName,
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