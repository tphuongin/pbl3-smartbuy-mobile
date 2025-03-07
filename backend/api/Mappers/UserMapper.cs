using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role ?? "user",
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToUser(this CreateUserRequestDTO createUserRequestDTO)
        {
            return new User
            {
                Name = createUserRequestDTO.Name,
                Email = createUserRequestDTO.Email,
                Password = createUserRequestDTO.Password,
                Gender = createUserRequestDTO.Gender,
                Phone = createUserRequestDTO.Phone,
                Address = createUserRequestDTO.Address,
                Role = createUserRequestDTO.Role,
                Avatar = createUserRequestDTO.Avatar
            };
        }
    }
}