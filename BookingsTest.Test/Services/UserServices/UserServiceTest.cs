using Bookings.Models.DB;
using Bookings.Services.UserServices;
using BookingsTest.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookingsTest.Test.Services.Userservices
{

    public class UserServiceTest : IDisposable
    {

        private readonly UserService _sut;
        private readonly BOOKINGS_DBContext _mockContext;
        private readonly MockUsers _mockUsers = new();

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<BOOKINGS_DBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockContext = new(options);
            _sut = new(_mockContext);
        }



        public void Dispose()
        {
            _mockContext.Database.EnsureDeleted();
            _mockContext.Dispose();
        }


        #region Test for GetAllUsers

        [Fact]
        public void GetAllUsers_ShouldReturnAListOfUsers()
        {
            //Arrange
            var data = _mockUsers.getUsers(5);
            _mockContext.Users.AddRange(data);
            _mockContext.SaveChanges();

            //Act
            var result =  _sut.GetAllUsers();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(data);
            result.Should().BeEquivalentTo(data, x => x.ExcludingMissingMembers());
        }


        [Fact]
        public void GetAllUsers_ShouldReturnTheCorrectCountOfUsers()
        {
            //Arrange
            var data = _mockUsers.getUsers(5);
            _mockContext.Users.AddRange(data);
            _mockContext.SaveChanges();

            //Act
            var result = _sut.GetAllUsers();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(data);
            result.Should().BeEquivalentTo(data, x => x.ExcludingMissingMembers());
            result.Should().HaveCount(data.Count);
        }


        [Fact]
        public void GetAllUsers_ShouldReturnNullIfNoListOfUsersIsFound()
        {
            //Arrange
            var data = _mockUsers.getUsers(0);
            _mockContext.Users.AddRange(data);
            _mockContext.SaveChanges();

            //Act
            var result = _sut.GetAllUsers();

            //Assert
            result.Should().BeEquivalentTo(data);
            result.Should().BeEquivalentTo(data, x => x.ExcludingMissingMembers());
            result.Should().BeNullOrEmpty();
        }


        #endregion



        #region Test for GetUserById

        [Theory]
        [InlineData(0)]
        public void GetUserById_ShouldReturnNullIfNoUserIdIsPassed(int id)
        {
            //Arrange
            var data = _mockUsers.getUsers(0).FirstOrDefault();

            //Act
            var result = _sut.GetUserById(id);

            //
            result.Should().BeNull();
            result.Should().BeEquivalentTo(data);
        }


        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetUserById_ShouldReturnNullIfNoUserWasFound(int id)
        {
            //Arrange
            var data = _mockUsers.getUsers(0).FirstOrDefault();

            //Act
            var result = _sut.GetUserById(It.IsAny<int>());

            //Assert
            result.Should().BeNull();
            result.Should().BeEquivalentTo(data);
            id.Should().NotBe(0);
            id.Should().BeOfType(typeof(int));
        }



       [Fact]
        public void GetUserById_ShouldReturnTheUserWhenFound()
        {
            //Arrange
            var data = _mockUsers.getUsers(1).FirstOrDefault();
            _mockContext.Users.Add(data);
            _mockContext.SaveChanges();

            //Act
            var result = _sut.GetUserById(data.UsersId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(data);
            result.Should().BeEquivalentTo(data, x => x.ComparingRecordsByValue().ComparingByMembers<IEnumerable<User>>());
         
        }

        #endregion



        [Fact]
        public async Task CreateUser_ShouldReturnUserObjectfUserModelIsEmpty()
        {
            //Arrange
            var data = _mockUsers.getUsers(0).FirstOrDefault();

            //Act
            var result = await _sut.CreateUser(data);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }



        [Fact]
        public async Task CreateUser_ShouldReturnUserObjectWithOnlyEmailIfUserExits()
        {
            //Arrange
            var data = _mockUsers.getUsers(1).FirstOrDefault();
            _mockContext.Users.Add(data);
            _mockContext.SaveChanges();

            //Act
            var result = await _sut.CreateUser(data);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.Should().NotBeEquivalentTo(data);
            result.Should().NotBeEquivalentTo(data, x => x.ComparingRecordsByValue().ComparingByMembers<User>());
            result.Email.Should().NotBeNullOrWhiteSpace();
            result.Email.Should().NotBeUpperCased();
            result.Email.Should().BeSameAs(data.Email);
        }


        [Fact]
        public async Task CreateUser_ShouldReturnUserObjectWhenCreated()
        {
            //Arrange
            var data = _mockUsers.getUsers(1).FirstOrDefault();
            
            //Act
            var result = await _sut.CreateUser(data);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.Should().BeEquivalentTo(data);
            result.Should().BeEquivalentTo(data, x => x.ComparingRecordsByValue().ComparingByMembers<User>());
        }
    }
}
