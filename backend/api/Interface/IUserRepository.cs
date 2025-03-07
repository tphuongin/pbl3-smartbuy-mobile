using api.Models;

namespace api.Interface
{
    public interface IUserRepository
    {
        Task<List<User>?> GetAsync();
        // Task<User?> GetByIDAsync(string id);
        // Task<User?> GetByEmailAsync(string email);
        // Task<User?> GetByPhoneAsync(string phone);
        // Task<User> CreateAsync(User user);
        // Task<User?> UpdateAsync(string id, UpdateUserRequestDTO userDTO);
        // Task<User?> DeleteAsync(string id);
    }
}