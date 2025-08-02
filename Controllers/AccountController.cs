using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.Services;
using Flare.AccountService.DTOs;

namespace Flare.AccountService.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAccountById([FromRoute] Guid userId)
    {
        var account = await _accountService.GetAccountByIdAsync(userId);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var response = await _accountService.CreateAccountAsync(request, userId);
        return Ok(response);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request, [FromRoute] Guid userId)
    {
        var response = await _accountService.UpdateAccountAsync(request, userId);
        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAccount([FromRoute] Guid userId, [FromQuery] bool hard = false)
    {
        await _accountService.DeleteAccountAsync(userId, hard);
        return NoContent();
    }
}