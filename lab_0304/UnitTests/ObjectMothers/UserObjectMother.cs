using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;

namespace UnitTests.ObjectMothers
{
    public class UserObjectMother
    {
        public UserBuilder CreateGuest()
        {
            return new UserBuilder()
                .WithRole("Guest")
                .WithName("Guest");
        }
        public UserBuilder CreateReferee()
        {
            return new UserBuilder()
                .WithRole("Referee")
                .WithName("Antony Taylor")
                .WithLogin("ref123")
                .WithPassword("@ref123");
        }
        public UserBuilder CreateAdmin()
        {
            return new UserBuilder()
                .WithRole("Admin")
                .WithName("Admin Handsome")
                .WithLogin("admin")
                .WithPassword("@admin");
        }
        public UserBuilder CreateUser(int id, string name)
        {
            return new UserBuilder()
                .WithName(name)
                .WithId(id)
                .WithRole("Referee");
        }
    }
}
