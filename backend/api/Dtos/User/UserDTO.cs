namespace api.Dtos.User
{
    public class UserDTO
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = "user";
        public byte[]? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}