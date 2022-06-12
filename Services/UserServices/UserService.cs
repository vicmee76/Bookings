using Bookings.Models.DB;
using System.Collections.Generic;
using System.Linq;

namespace Bookings.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly BOOKINGS_DBContext _context;

        public UserService(BOOKINGS_DBContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }


        public IEnumerable<User> GetUserById(int? v)
        {
            throw new System.NotImplementedException();
        }

    }
}
