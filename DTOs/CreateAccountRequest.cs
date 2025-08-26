using System.ComponentModel.DataAnnotations;

namespace Flare.AccountService.DTOs;

public class CreateAccountRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public required string Username { get; set; }

    [MaxLength(50)]
    public string? DisplayName { get; set; }
}
