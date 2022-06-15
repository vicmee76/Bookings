using Bookings.Controllers;
using Bookings.Models.DB;
using Bookings.Services.UserServices;
using BookingsTest.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookingsTest.Test.Controllers
{
    public class UsersControllerTest
    {
        private readonly UserController _sut;
        private readonly Mock<IUserService> _mockUserService;
        private readonly MockUsers usersResult = new();

        public UsersControllerTest()
        {
            _mockUserService = new();
            _sut = new(_mockUserService.Object);
        }


        #region Test GetUsers Controller Method

        [Fact]
        public void GetUsers_OnSuucess_ReturnOkResult()
        {
            var mockUsers = usersResult.getUsers(5);
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(mockUsers);

            var result = (OkObjectResult)_sut.GetAllUsers();

            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public void GetUsers_ShouldInvokeIGetUserService()
        {
            var mockUsers = usersResult.getUsers(5);
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(mockUsers);

            var result = _sut.GetAllUsers();

            _mockUserService.Verify(x => x.GetAllUsers(), Times.Once);

        }



        [Fact]
        public void GetUsers_OnSuccess_ShouldReturnListOfUsers()
        {
            var mockUsers = usersResult.getUsers(5);
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(mockUsers);

            var result = _sut.GetAllUsers();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }


        [Fact]
        public void GetUsers_OnFailure_ShouldReturnNotFound()
        {
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(new List<User>());

            var result = _sut.GetAllUsers();

            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public void GetUsers_OnFailure_ShouldReturn404NotFound()
        {
            _mockUserService.Setup(x => x.GetAllUsers()).Returns(new List<User>());

            var result = _sut.GetAllUsers();

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        #endregion




        #region Test GetUserById Controller Method

        [Fact]
        public void GetUserById_ShouldBeCalledOnce()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(new User());

            var result = _sut.GetUserById(It.IsAny<int>());

            _mockUserService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.AtLeastOnce);
        }



        [Fact]
        public void GetUserById_ShouldReturnBadRequest_IfNoUserIdIsPassed()
        {
            _mockUserService.Setup(x => x.GetUserById(null)).Returns(new User());

            var result = _sut.GetUserById(null);

            result.Should().BeOfType<BadRequestResult>();
            var objectResult = (BadRequestResult)result;
            objectResult.StatusCode.Should().Be(400);
        }



        [Theory]
        [InlineData(3)]
        public void GetUserById_ShouldReturnNotFound_IfTheUserIdDoesNotExits(int id)
        {
            var mockUsers = usersResult.getUsers(0).FirstOrDefault();
            _mockUserService.Setup(x => x.GetUserById(id)).Returns(mockUsers);

            var result = _sut.GetUserById(id);

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }


        [Fact]
        public void GetUserById_ShouldReturnOkResult_IfTheUserWasFound()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();

            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mockUsers);

            var result = _sut.GetUserById(It.IsAny<int>());

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }


        [Fact]
        public void GetUserById_ShouldReturnTheUser_IfTheUserWasFound()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();

            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mockUsers);

            var result = _sut.GetUserById(It.IsAny<int>());

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeEquivalentTo(mockUsers, x => x.ComparingByMembers<User>());
        }



        [Fact]
        public void GetUserById_ShouldReturnTheCountOfUser_IfTheUserWasFound()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();

            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mockUsers);

            var result = _sut.GetUserById(It.IsAny<int>());

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            mockUsers.Should().Be(objectResult.Value);
            mockUsers.Should().BeEquivalentTo(objectResult.Value, x => x.ExcludingMissingMembers());

        }

        #endregion



        #region CreateUser Controller Method


        [Fact]
        public async Task CreateUser_ShouldInvokeCreateUserService()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();
            _mockUserService.Setup(x => x.CreateUser(mockUsers)).ReturnsAsync(mockUsers);

            var result = await _sut.CreateUser(mockUsers);

            _mockUserService.Verify(x => x.CreateUser(mockUsers), Times.AtLeastOnce);
        }


        [Fact]
        public async Task CreateUser_ShouldReturnNoContentResultIfEmptyUserModelIsPassed()
        {
            var mockUsers = usersResult.getUsers(0).FirstOrDefault();
            _mockUserService.Setup(x => x.CreateUser(mockUsers)).ReturnsAsync(mockUsers);

            var result = await _sut.CreateUser(mockUsers);

            result.Should().BeOfType<NoContentResult>();
            var objectResult = (NoContentResult)result;
            objectResult.StatusCode.Should().Be(204);
            mockUsers.Should().BeNull();
        }



        [Fact]
        public async Task CreateUser_ShouldOKIfUserWasCreated()
        {
            var mockUsers = usersResult.getUsers(1).FirstOrDefault();
            _mockUserService.Setup(x => x.CreateUser(mockUsers)).ReturnsAsync(mockUsers);

            var result = await _sut.CreateUser(mockUsers);

            result.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)result;
            objectResult.StatusCode.Should().Be(201);
            objectResult.Value.Should().NotBeNull();
            objectResult.Value.Should().BeOfType<User>();
            objectResult.Value.Should().BeEquivalentTo(mockUsers, x => x.ComparingRecordsByValue().ComparingByMembers<User>());
        }


        #endregion

    }
}
