using Bookings.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookings.Services.UserServices
{
    public class UserService : IUserService
    {
       

        public UserService()
        {

        }

        public Task<List<User>> GetAllUsers()
        {
            return null;
        }
    }
}
