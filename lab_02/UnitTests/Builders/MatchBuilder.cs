using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class MatchBuilder
    {
        private int _id;
        private int _goalHome;
        private int _goalGuest;
        private int _idLeague;
        private int _idHome;
        private int _idGuest;
        public MatchBuilder()
        {
            _id = 1;
            _goalHome = 3;
            _goalGuest = 1;
            _idLeague = 1;
            _idHome = 1;
            _idGuest = 2;
        }
        public MatchBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public MatchBuilder WithGoalHome(int goalHome)
        {
            _goalHome = goalHome;
            return this;
        }
        public MatchBuilder WithGoalGuest(int goalGuest)
        {
            _goalGuest = goalGuest;
            return this;
        }
        public MatchBuilder WithIdLeague(int idLeague)
        {
            _idLeague = idLeague;
            return this;
        }
        public MatchBuilder WithIdHome(int idHome)
        {
            _idHome = idHome;
            return this;
        }
        public MatchBuilder WithIdGuest(int idGuest)
        {
            _idGuest = idGuest;
            return this;
        }
        public Match BuildCoreModel()
        {
            return new Match(_idLeague, _idHome, _idGuest, _goalHome, _goalGuest, _id);
        }
    }
}
