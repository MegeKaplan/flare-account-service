namespace Flare.AccountService.DTOs;

public class CreateAccountResponse
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public DateTime CreatedAt { get; set; }
}
