using System.ComponentModel.DataAnnotations;

namespace Flare.AccountService.DTOs;

public class UpdateAccountRequest
{
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(6)]
    public string? Password { get; set; }

    [MinLength(3)]
    [MaxLength(20)]
    public string? Username { get; set; }

    [MaxLength(50)]
    public string? DisplayName { get; set; }
}
