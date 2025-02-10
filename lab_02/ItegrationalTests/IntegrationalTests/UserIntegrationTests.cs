using Allure.Xunit.Attributes;
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
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("UserIntegrational Tests")]
    public class UserIntegrationTests
    {
        private IntegrationFixture _fixture;
        private UserService _userService;
        private UserObjectMother userOM = new UserObjectMother();
        public UserIntegrationTests()
        {
            _fixture = new IntegrationFixture();
            var _userRepo = new UserRepository(_fixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            _userService = new UserService(_userRepo, NullLogger<UserService>.Instance);
        }
        [SkippableFact]
        public void TestLogin()
        {
            Skip.If(IntegrationFixture.SkipTest);
            //Console.WriteLine(IntegrationFixture.SkipTest);
            //Console.WriteLine("hehehe" + IntegrationFixture.TestSkipTest);
            var users = _fixture.AddUsers();
            var user = users.First();

            var actual = _userService.Login(user.Login, user.Password);

            Assert.Equal(user.Login, actual.Login);
            Assert.Equal(user.Password, actual.Password);
        }
        [SkippableFact]
        public void TestRegister()
        {
            Skip.If(IntegrationFixture.SkipTest);
            //Console.WriteLine(IntegrationFixture.SkipTest);
            //Console.WriteLine("hehehe" + IntegrationFixture.TestSkipTest);

            var user = userOM.CreateReferee().WithName("testname").WithLogin("testlogin123").BuildCoreModel();
            var cnt = _fixture._dbContextFactory.get_db_context().users.Count();

            _userService.Register(user.Login, user.Password, user.Role, user.Name);

            Assert.Equal(cnt + 1, _fixture._dbContextFactory.get_db_context().users.Count());
        }
        [SkippableFact]
        public void TestChangeInfo()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var users = _fixture.AddUsers();
            var u = userOM.CreateReferee().WithId(users.First().Id).WithName("mytest").WithLogin("test123").BuildCoreModel();

            _userService.ChangeInfo(u.Id, u.Login, u.Password, u.Role, u.Name);

            var user = _userService.getbyId(users.First().Id);
            Assert.Equal("mytest", user.Name);
            Assert.Equal("test123", user.Login);
        }
        [SkippableFact]
        public void TestGetAll()
        {
            var users = _fixture.AddUsers();

            var actual = _userService.getAll();

            Assert.Equal(users.Count(), actual.Count);

        }
    }
}
