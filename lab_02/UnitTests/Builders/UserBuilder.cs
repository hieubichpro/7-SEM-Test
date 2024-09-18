using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class UserBuilder
    {
        private int _id;
        private string _login;
        private string _password;
        private string _role;
        private string _name;
        public UserBuilder()
        {
            _id = 1;
            _login = "test";
            _password = "test";
            _role = "test";
            _name = "test";
        }
        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public UserBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }
        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }
        public UserBuilder WithRole(string role)
        {
            _role = role;
            return this;
        }
        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public User BuildCoreModel()
        {
            return new User(_login, _password, _role, _name, _id);
        }
    }
}
