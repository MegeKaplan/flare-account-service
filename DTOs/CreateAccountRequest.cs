using System.ComponentModel.DataAnnotations;

namespace Flare.AccountService.DTOs;

public class CreateAccountRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; }
}
