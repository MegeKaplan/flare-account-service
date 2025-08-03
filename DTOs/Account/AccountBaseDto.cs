
namespace Flare.AccountService.DTOs.Account
{
    public class AccountBaseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}