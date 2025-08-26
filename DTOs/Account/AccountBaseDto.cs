
namespace Flare.AccountService.DTOs.Account
{
    public class AccountBaseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImageId { get; set; }
        public string BannerImageId { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}