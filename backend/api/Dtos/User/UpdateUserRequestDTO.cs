using System.Text.Json.Serialization;

namespace api.Dtos.User
{
    public class UpdateUserRequestDTO
    {
        // JsonIgnore được sử dụng để bỏ qua các thuộc tính không cần thiết trong quá trình tuần tự hóa JSON
        // JsonIgnoreCondition.WhenWritingNull chỉ định rằng thuộc tính sẽ bị bỏ qua nếu giá trị của nó là null
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Gender { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Phone { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Address { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[]? Avatar { get; set; }
    }
}