// Models là lớp dùng để định nghĩa các thực thể trong cơ sở dữ liệu
namespace api.Models
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; } = "user";
        public byte[]? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}