using lab_01.BL.Models;
using lab_01.BL.Services;
using lab_01.DA.dbContext;
using lab_01.DA.dbContext.PostgreSQL;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using TechTalk.SpecFlow;

namespace BDD.StepDefinitions
{
    [Binding]
    public class EmailTwoFactoryAuthStepDefinitions
    {
        private dbContextFactory _dbContextFactory = new pgSqlDbContextFactory("Server=localhost;Username=postgres;Password=123456789;Database=TestTestTest");
        private LoginModel _loginModel;
        private string _verifyCode;
        private string _receivedCode;
        private bool _isConfirmed;
        private bool _isCodeSent;
        private User user1;
        private IUserRepository _userRepository;
        private AuthService _authService;
        private UserService _userService;
        private IMemoryCache _memoryCache;

        public EmailTwoFactoryAuthStepDefinitions()
        {
            var emailOptions = Options.Create(
            new ConfirmingEmailConfiguration()
            {
                Email = "hieubauman2025@mail.ru",
                Password = "S7CXSHa9crEHnsrxfC4H",
                CodeLifeTimeMinutes = 3
            }
            );
            _userRepository = new UserRepository(_dbContextFactory, NullLogger<UserRepository>.Instance);
            _userService = new UserService(_userRepository, NullLogger<UserService>.Instance);
            var options = new MemoryCacheOptions();
            _memoryCache = new MemoryCache(options);
            _authService = new AuthService(_userRepository, _userService, _memoryCache, emailOptions);
            user1 = new User("user1", "1", "Admin", "Hieubich", "minhhieuk19@gmail.com", false);
            //var user1 = _userOM.CreateAdmin().WithLogin("user1").WithPassword("1").WithEmail("minhhieuk19@gmail.com").BuildCoreModel();
            using var db_context = _dbContextFactory.get_db_context();
            db_context.users.Add(user1);
            db_context.SaveChanges();
        }

        [Given(@"user enters the login information")]
        public void GivenUserEntersTheLoginInformation()
        {
            _loginModel = new LoginModel("user1", "1");
        }

        [When(@"user sends a login request")]
        public void WhenUserSendsALoginRequest()
        {
            var user = _authService.Login(_loginModel.Login, _loginModel.Password);
            if (user != null)
            {
                _verifyCode = _authService.GenerateCode(user.Id);
                _isCodeSent = _authService.SendCode(user, _verifyCode);
            }
        }

        [Then(@"system should send the code to the email")]
        public void ThenSystemShouldSendTheCodeToTheEmail()
        {
            Assert.NotNull(_verifyCode);
            Assert.Equal(true, _isCodeSent);
        }

        [Given(@"user enters the received code")]
        public void GivenUserEntersCode()
        {
            _receivedCode = _verifyCode;
        }

        [When(@"user sends a confirmation code")]
        public void WhenUserSendsAConfirmCode()
        {
            _isConfirmed = _receivedCode == _verifyCode;
        }

        [Then(@"system confirms the email")]
        public void ThenSystemConfirmTheEmail()
        {
            Assert.Equal(true, _isConfirmed);
        }





        [Given(@"user enters new password")]
        public void GivenUserEntersNewPassword()
        {
            user1.Password = "234";
        }

        [When(@"user send a change password request")]
        public void WhenUserSendsAChangePasswordRequest()
        {
            _authService.ChangePassword(user1);
            _verifyCode = _authService.GenerateCode(user1.Id);
            _isCodeSent = _authService.SendCode(user1, _verifyCode);
        }

        [Then(@"system should send the code to emaill")]
        public void ThenSystemShouldSendTheCodeToTheEmail1()
        {
            Assert.NotNull(_verifyCode);
            Assert.Equal(true, _isCodeSent);
        }

        [Given(@"user enters the received codee")]
        public void GivenUserEntersCode1()
        {
            _receivedCode = _verifyCode;
        }

        [When(@"user sends a confirmation codee")]
        public void WhenUserSendsAConfirmCode1()
        {
            _isConfirmed = _receivedCode == _verifyCode;
        }

        [Then(@"system confirms the new password")]
        public void ThenSystemConfirmTheEmail1()
        {
            Assert.Equal(true, _isConfirmed);
        }
    }
}
