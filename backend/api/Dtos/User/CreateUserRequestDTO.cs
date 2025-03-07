namespace api.Dtos.User
{
    public class CreateUserRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string Role { get; set; } = "user";
        public byte[]? Avatar { get; set; }
    }
}