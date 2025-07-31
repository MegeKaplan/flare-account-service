using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

using Flare.AccountService.Repositories;
using Flare.AccountService.DTOs;
using Flare.AccountService.Models;

namespace Flare.AccountService.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
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

}