using Bookings.Models.DB;
using Bookings.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Services.Books
{
    public class Books : IBooks
    {
        private IUserService _userService;
        private readonly BOOKINGS_DBContext _context;

        public Books(IUserService userService, BOOKINGS_DBContext context)
        {
            _userService = userService;
            _context = context;
        }



        public Book CreateUsersBooks(int user, Book book)
        {
            var get = _userService.GetUserById(user);

            if (get is null)
                return null;

            _context.Books.Add(book);
             _context.SaveChanges();
            return book;
        }
    }
}
