using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using Castle.Core.Logging;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;
using Xunit;

namespace UnitTests.UnitTests.TestBL
{
[AllureOwner("Hieu Bauman")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("UserServices Unit tests")]
[AllureSubSuite("UserService unit tests London Method")]

    public class UserServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private UserObjectMother userOM = new UserObjectMother();
        [AllureBefore]
        public UserServiceUnitTests() { }
        [Fact]
        public void TestLoginSuccess()
        {
            var user = userOM.CreateUser(7, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readByLogin(user.Login)).Returns(user);
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            var actual = userService.Login(user.Login, user.Password);

            Assert.Equal(user, actual);
            _userRepoMock.Verify(m => m.readByLogin(user.Login), Times.Once());
        }
        [Fact]
        public void TestLoginFailure()
        {
            var user = userOM.CreateUser(7, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readByLogin(user.Login)).Throws(new UserNotFoundException());
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.Login(user.Login, user.Password));
            _userRepoMock.Verify(m => m.readByLogin(user.Login), Times.Once());
        }
        //users.FirstOrDefault(u => u.Login  == user.Login) == null
        //[Fact]
        //public void TestRegisterSuccess()
        //{
        //    var users = fixture.PrepareUsersForTest();
        //    var user = userOM.CreateReferee().WithId(100).WithLogin("abcaapp").BuildCoreModel();
        //    Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
        //    //_userRepoMock.Setup(m => m.readByLogin(user.Login)).Returns(users.FirstOrDefault(u => u.Login == user.Login));
        //    _userRepoMock.Setup(m => m.create(user)).Callback(() => users.Add(user));

        //    var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

        //    userService.Register(user.Login, user.Password, user.Role, user.Name);

        //    _userRepoMock.Verify(m => m.create(user), Times.Once());
        //    //Assert.Equal(11, users.Count);
        //    //_userRepoMock.Verify(m => m.readByLogin(user.Login), Times.Once());
        //}
        [Fact]
        public void TestRegisterFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users.First();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

            _userRepoMock.Setup(m => m.create(user)).Callback(() => users.Add(user));
            _userRepoMock.Setup(m => m.readByLogin(user.Login)).Returns(users.FirstOrDefault(u => u.Login == user.Login));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            Assert.Throws<UserExistException>(() => userService.Register(user.Login, user.Password, user.Role, user.Name));
            _userRepoMock.Verify(m => m.readByLogin(user.Login), Times.Once());
        }
        [Fact]
        public void TestChangeInfoSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.update(user)).Callback(() =>
            {
                user.Name = "123";
                user.Login = "abc";
            });
            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            userService.ChangeInfo(user.Id, user.Login, user.Password, user.Role, user.Name);

            Assert.Equivalent(user.Name, "123");
            Assert.Equivalent(user.Login, "abc");
            _userRepoMock.Verify(m => m.readById(user.Id), Times.Once());
            _userRepoMock.Verify(m => m.update(user), Times.Once());
        }
        [Fact]
        public void TestChangeInfoFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateUser(11, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.update(user)).Callback(() =>
            {
                user.Name = "123";
                user.Login = "abc";
            });
            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.ChangeInfo(user.Id, user.Login, user.Password, user.Role, user.Name));
            _userRepoMock.Verify(m => m.readById(user.Id), Times.Once());
            _userRepoMock.Verify(m => m.update(user), Times.Never());
        }
        [Fact]
        public void TestGetAll()
        {
            string role = "Referee";
            var users = fixture.PrepareUsersForTest();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readByRole(role)).Returns(users);
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            var actual = userService.getAll();

            Assert.Equal(users.Count, actual.Count);
            _userRepoMock.Verify(m => m.readByRole(role), Times.Once());
        }
        [Fact]
        public void TestGetUserByIdSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            var actual = userService.getbyId(user.Id);

            Assert.Equal(user, actual);
            _userRepoMock.Verify(m => m.readById(user.Id), Times.Once());
        }

        [Fact]
        public void TestGetUserByIdFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateReferee().WithId(11).BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.getbyId(user.Id));
            _userRepoMock.Verify(m => m.readById(user.Id), Times.Once());
        }

        [Fact]
        public void TestDeleteUserSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.delete(user)).Callback(() => users.Remove(user));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);

            userService.deleteUser(user);

            Assert.Equal(9, users.Count);
            _userRepoMock.Verify(m => m.delete(user), Times.Once());
        }
        [Fact]
        public void TestDeleteUserFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateReferee().WithId(11).BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.readById(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.delete(user)).Callback(() => users.Remove(user));
            var userService = new UserService(_userRepoMock.Object, NullLogger<UserService>.Instance);


            Assert.Throws<UserNotFoundException>(() => userService.deleteUser(user));
            _userRepoMock.Verify(m => m.readById(user.Id), Times.Once());
            _userRepoMock.Verify(m => m.delete(user), Times.Never());
        }
    }
}
