using Bookings.Models.DB;
using Bookings.Services.Books;
using Bookings.Services.UserServices;
using BookingsTest.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingsTest.Test.Services.Book
{
    public class BookServiceTest : IDisposable
    {
        private readonly Books _sut;

        private readonly BOOKINGS_DBContext _mockContext;
        private readonly Mock<IUserService> _mockUserService;

        private readonly MockUsers _mockUsers = new();

        public BookServiceTest()
        {
            var options = new DbContextOptionsBuilder<BOOKINGS_DBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockUserService = new();
            _mockContext = new(options);

            _sut = new(_mockUserService.Object, _mockContext);

        }


        public void Dispose()
        {
            _mockContext.Database.EnsureDeleted();
            _mockContext.Dispose();
        }


        [Fact]
        public void GetUsersBooks_OnSuucess_ReturnBooks()
        {
            var mockUsers = _mockUsers.getUsers(1).FirstOrDefault();
            _mockUserService.Setup(x => x.GetUserById(1)).Returns(mockUsers);

            Bookings.Models.DB.Book book = new Bookings.Models.DB.Book()
            {
                UserId = mockUsers.UsersId,
                BookName = "Another Book"
            };

            var result = _sut.CreateUsersBooks(1, book);

            result.Should().NotBeNull();
        }


    }
}
