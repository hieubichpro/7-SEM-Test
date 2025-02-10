using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;

namespace UnitTests.Fixture
{
    public class ServiceFixture
    {
        public ServiceFixture() { }
        public List<User> PrepareUsersForTest()
        {
            var userOM = new UserObjectMother();
            var users = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                users.Add(userOM.CreateReferee().BuildCoreModel());
            }
            return users;
        }

        public List<Match> PrepareMatchesForTest()
        {
            var matchOM = new MatchObjectMother();
            var matches = new List<Match>();
            for (int i = 0;i < 10;i++)
            {
                matches.Add(matchOM.CreateMatch().BuildCoreModel());
            }
            return matches;
        }

        public List<League> PrepareLeaguesForTest()
        {
            var leagueOM = new LeagueObjectMother();
            var leagues = new List<League>();
            for (int i = 0; i < 10 ; i++)
            {
                leagues.Add(leagueOM.CreateLeague().BuildCoreModel());
            }
            return leagues;
        }
        public List<Club> PrepareClubsForTest()
        {
            var clubOM = new ClubObjectMother();
            var clubs = new List<Club>();
            for (int i = 0; i < 10; i++)
            {
                clubs.Add(clubOM.CreateClub().BuildCoreModel());
            }
            return clubs;
        }
    }
}
