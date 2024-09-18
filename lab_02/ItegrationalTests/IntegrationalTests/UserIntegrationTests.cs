using ItegrationalTests.Fixture;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Xunit;

namespace ItegrationalTests.IntegrationalTests
{
    public class UserIntegrationTests : IntegrationFixture
    {
        private UserService _userService;
        public UserIntegrationTests()
        {
            var _userRepo = new UserRepository(_dbContextFactory, NullLogger<UserRepository>.Instance);
            _userService = new UserService(_userRepo, NullLogger<UserService>.Instance);
        }
        [SkippableFact]
        public void TestLogin()
        {
            string login = "admin";
            string password = "1";

            var user = _userService.Login(login, password);

            Assert.Equal(login, user.Login);
            Assert.Equal(password, user.Password);
            Assert.Equal("Admin", user.Role);
            Assert.Equal(5, user.Id);
        }
        [SkippableFact]
        public void TestRegister()
        {
            Skip.If(SkipTest);
            var user = userOM.CreateReferee().WithName("testname").WithLogin($"testlogin {Guid.NewGuid()}".Substring(25)).BuildCoreModel();
            var cnt = _dbContextFactory.get_db_context().users.Count();

            _userService.Register(user.Login, user.Password, user.Role, user.Name);

            Assert.Equal(cnt + 1, _dbContextFactory.get_db_context().users.Count());
        }
        [SkippableFact]
        public void TestChangeInfo()
        {
            Skip.If(SkipTest);
            var u = userOM.CreateReferee().WithId(1).WithName("mytest").WithLogin("test123").BuildCoreModel();

            _userService.ChangeInfo(u.Id, u.Login, u.Password, u.Role, u.Name);

            var user = _userService.getbyId(1);
            Assert.Equal("mytest", user.Name);
            Assert.Equal("test123", user.Login);
        }
        [SkippableFact]
        public void TestGetAll()
        {
            var users = _dbContextFactory.get_db_context().users.Where(u => u.Role == "Referee");

            var actual = _userService.getAll();

            Assert.Equal(users.Count(), actual.Count);

        }
    }
}
