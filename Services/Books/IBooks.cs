using Bookings.Models.DB;

namespace Bookings.Services.Books
{
    public interface IBooks
    {
        Book CreateUsersBooks(int user, Book book);
    }
}
