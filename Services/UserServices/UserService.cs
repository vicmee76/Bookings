using Bookings.Models.DB;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> CreateUser(User user)
        {
            User users = new();
            if (user == null)
            {
                return users;
            }
            else
            {
                var getUser = await _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
                if (getUser != null)
                {
                    users.Email = getUser.Email;
                    return users;
                }
                else
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
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
