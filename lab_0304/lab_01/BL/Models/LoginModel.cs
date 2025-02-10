using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.BL.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
