using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class LeagueBuilder
    {
        private int _id;
        private string _name;
        private int _id_user;
        public LeagueBuilder()
        {
            _id = 1;
            _name = "testLeague";
            _id_user = 1;
        }
        public LeagueBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public LeagueBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public LeagueBuilder WithIdUser(int id_user)
        {
            _id_user = id_user;
            return this;
        }
        public League BuildCoreModel()
        {
            return new League(_name, _id_user, _id);
        }
    }
}
