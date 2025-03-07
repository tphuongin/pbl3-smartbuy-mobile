using api.Dtos.User;

namespace api.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}