using api.Dtos.User;
using api.Interfaces;
using api.Mappers;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        public UserService(IUserRepository userRepository)
        {
            repo = userRepository;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await repo.GetAsync();
            if (users == null)
            {
                throw new KeyNotFoundException("No users found.");
            }
            return users.Select(user => user.ToUserDTO()).ToList();
        }
    }
}