using api.Dtos.User;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}