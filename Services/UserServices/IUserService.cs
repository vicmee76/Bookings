using Bookings.Models.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Services.UserServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int? v);
    }
}
