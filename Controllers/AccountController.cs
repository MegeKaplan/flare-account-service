using Microsoft.AspNetCore.Mvc;

using Flare.AccountService.Services;

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
    public async Task<IActionResult> CreateAccount()
    {
        var result = await _accountService.CreateAccountAsync();
        return Ok(result);
    }
}