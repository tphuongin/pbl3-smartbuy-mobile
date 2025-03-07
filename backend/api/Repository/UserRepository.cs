using api.Database;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDB db;
        public UserRepository(ApplicationDB db)
        {
            this.db = db;
        }
        public async Task<List<User>?> GetAsync()
        {
            return await db.Users.ToListAsync();
        }
    }
}