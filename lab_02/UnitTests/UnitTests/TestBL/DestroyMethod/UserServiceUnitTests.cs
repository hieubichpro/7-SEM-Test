using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using Castle.Core.Logging;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;
using Xunit;

namespace UnitTests.UnitTests.TestBL.DestroyMethod
{
    [AllureOwner("Hieu Bauman")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("UserServices Unit tests")]
    [AllureSubSuite("UserService unit tests Destroit Method")]
    public class UserServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private UserObjectMother userOM = new UserObjectMother();
        [AllureBefore]
        public UserServiceUnitTests() { }
        [Fact]
        public void TestLoginSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            var actual = userService.Login(user.Login, user.Password);

            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestLoginFailureDestroitMethod()
        {
            var user = userOM.CreateReferee().WithId(200).WithLogin("test 200").WithPassword("test 200").BuildCoreModel();
            fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.Login(user.Login, user.Password));
        }
        [Fact]
        public void TestRegisterSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateReferee().WithId(11).WithLogin("abcaapp").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            userService.Register(user.Login, user.Password, user.Role, user.Name);

            var actual = _userRepo.readByLogin(user.Login);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestRegisterFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            Assert.Throws<UserExistException>(() => userService.Register(user.Login, user.Password, user.Role, user.Name));
        }
        [Fact]
        public void TestChangeInfoSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateReferee().WithId(users.First().Id).WithLogin("123").WithName("1234").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            userService.ChangeInfo(user.Id, user.Login, user.Password, user.Role, user.Name);

            var actual = _userRepo.readByLogin(user.Login);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestChangeInfoFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateReferee().WithId(100).WithLogin("abcaapp").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);


            Assert.Throws<UserNotFoundException>(() => userService.ChangeInfo(user.Id, user.Login, user.Password, user.Role, user.Name));
        }
        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            var actual = userService.getAll();

            Assert.Equivalent(users, actual);

        }
        [Fact]
        public void TestGetUserByIdSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            var actual = userService.getbyId(user.Id);

            Assert.Equivalent(user, actual);
        }

        [Fact]
        public void TestGetUserByIdFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateReferee().WithId(100).WithLogin("123").WithName("1234").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.getbyId(user.Id));
        }

        [Fact]
        public void TestDeleteUserSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            userService.deleteUser(user);

            var actual = _userRepo.readById(user.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestDeleteUserFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateReferee().WithId(100).WithLogin("123").WithName("1234").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            var userService = new UserService(_userRepo, NullLogger<UserService>.Instance);

            Assert.Throws<UserNotFoundException>(() => userService.deleteUser(user));
        }
    }
}
