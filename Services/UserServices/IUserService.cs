using Bookings.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookings.Services.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
