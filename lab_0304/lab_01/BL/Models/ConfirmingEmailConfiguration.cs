using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.BL.Models
{
    public class ConfirmingEmailConfiguration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int CodeLifeTimeMinutes {  get; set; }
        public ConfirmingEmailConfiguration() { }
        public ConfirmingEmailConfiguration(string email, string password, int codeLifeTimeMinutes)
        {
            Email = email;
            Password = password;
            CodeLifeTimeMinutes = codeLifeTimeMinutes;
        }
    }
}
