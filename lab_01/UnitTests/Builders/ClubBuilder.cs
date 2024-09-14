using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class ClubBuilder
    {
        private int _id;
        private string _name;
        public ClubBuilder()
        {
            _id = 1;
            _name = "TestClub";
        }
        public ClubBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public ClubBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public Club BuildCoreModel()
        {
            return new Club(_name, _id);
        }
    }
}
