using Bookings.Models.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly BOOKINGS_DBContext _context;

        public UserService(BOOKINGS_DBContext context)
        {
            _context = context;
        }

        public Task<User> CreateUser(User mockUsers)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetUserById(int? v)
        {
            User user = new();
            if (v is null)
                return user;

             user = _context.Users.Where(x => x.UsersId == v).FirstOrDefault();
            return user;
        }
    }
}
