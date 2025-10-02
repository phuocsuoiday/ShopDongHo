using ShopDongHo.Models.Entities;
using System.Linq;

namespace ShopDongHo.Models.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ShopDbContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.IsActive);
        }

        public User GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email && u.IsActive);
        }

        public bool IsUsernameExists(string username)
        {
            return _dbSet.Any(u => u.Username == username);
        }

        public bool IsEmailExists(string email)
        {
            return _dbSet.Any(u => u.Email == email);
        }
    }
}
