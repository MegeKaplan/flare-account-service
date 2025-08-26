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

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAccountById([FromRoute] Guid userId)
    {
        var account = await _accountService.GetAccountByIdAsync(userId);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetAccountByUsername([FromRoute] string username)
    {
        var account = await _accountService.GetAccountByUsernameAsync(username);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, [FromRoute] Guid userId)
    {
        var principalUserIdHeader = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();

        if (!Guid.TryParse(principalUserIdHeader, out Guid principalUserId)) return Forbid();

        var response = await _accountService.CreateAccountAsync(request, principalUserId, userId);
        return Ok(response);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request, [FromRoute] Guid userId)
    {
        var principalUserIdHeader = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();

        if (!Guid.TryParse(principalUserIdHeader, out Guid principalUserId)) return Forbid();

        var response = await _accountService.UpdateAccountAsync(request, principalUserId, userId);
        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAccount([FromRoute] Guid userId, [FromQuery] bool hard = false)
    {
        var principalUserIdHeader = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();

        if (!Guid.TryParse(principalUserIdHeader, out Guid principalUserId)) return Forbid();

        await _accountService.DeleteAccountAsync(principalUserId, userId, hard);
        return NoContent();
    }
}