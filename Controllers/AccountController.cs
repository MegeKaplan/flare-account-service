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

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var response = await _accountService.CreateAccountAsync(request, userId);
        return Ok(response);
    }
}