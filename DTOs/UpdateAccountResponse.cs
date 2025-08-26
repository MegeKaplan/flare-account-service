namespace Flare.AccountService.DTOs;

public class UpdateAccountResponse
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public string? DisplayName { get; set; }
    public string? ProfileImageId { get; set; }
    public string? BannerImageId { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}