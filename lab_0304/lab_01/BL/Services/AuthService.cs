using lab_01.BL.Exceptions;
using lab_01.BL.Models;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.BL.Services
{
    public class AuthService
    {
        private IUserRepository _userRepository;
        private UserService _userService;
        private IMemoryCache _memoryCache;
        private ConfirmingEmailConfiguration _confirmingEmailConfiguration;

        public AuthService(IUserRepository userRepository, UserService userService, IMemoryCache memoryCache, IOptions<ConfirmingEmailConfiguration> confirmingEmailConfiguration)
        {
            _userRepository = userRepository;
            _userService = userService;
            _memoryCache = memoryCache;
            _confirmingEmailConfiguration = confirmingEmailConfiguration.Value;
        }
        public void Registrate(User user)
        {
            var u = _userRepository.readByEmail(user.Email);
            if (u == null)
            {
                _userRepository.create(user);
            }
            else
            {
                throw new UserExistException();
            }
        }

        public User Login(string login, string password)
        {
            var u = _userRepository.readByLogin(login);
            if (u != null)
            {
                if (u.checkPassword(password))
                    return u;
                else
                    throw new UserNotMatchPasswordException();
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
        public void ChangePassword(User user)
        {
            _userRepository.update(user);
        }
        public string GenerateCode(int id)
        {
            string code = "";
            if (_memoryCache.TryGetValue(id, out var realCode))
            {
                return realCode?.ToString() ?? throw new Exception("Code is null.");
            }
            else
            {
                var bytes = new byte[6];
                var rng = new Random();
                rng.NextBytes(bytes);

                code = Convert.ToBase64String(bytes);

                _memoryCache.Set(id, code, TimeSpan.FromMinutes(_confirmingEmailConfiguration.CodeLifeTimeMinutes));
            }
            return code;
        }
        public string? GetCode(int id)
        {
            return _memoryCache.TryGetValue(id, out var realCode) ? realCode?.ToString() : null;
        }

        public bool VerifyCode(int id, string code)
        {
            if (_memoryCache.TryGetValue(id, out var realCode))
            {
                _userService.ConfirmEmail(id);

                return code == realCode?.ToString();
            }

            return false;

        }
        public bool SendCode(User user, string code)
        {
            try
            {
                //string emailFrom = Environment.GetEnvironmentVariable("Email") ?? _emailConfiguration.Email;
                //string password = Environment.GetEnvironmentVariable("EmailPassword") ?? _emailConfiguration.Password;
                string emailFrom = _confirmingEmailConfiguration.Email;
                string password = _confirmingEmailConfiguration.Password;

                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("League - Tournaments", emailFrom));
                mail.To.Add(new MailboxAddress(user.Email, user.Email));
                mail.Subject = "Подтверждения почты";
                mail.Body = new TextPart("Plain")
                {
                    Text = $"Код подтверждения: {code}. У вас около {_confirmingEmailConfiguration.CodeLifeTimeMinutes} минут(ы) для того, чтобы вести код, иначе придется запросить новый код."
                };

                using var client = new SmtpClient();
                client.Connect("smtp.mail.ru", 465, true);

                client.Authenticate(emailFrom, password);
                client.Send(mail);

                client.Disconnect(true);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error while sending code to {user.Email}");
            }

        }
    }
}
