using lab_01.DA.dbContext;
using lab_01.DA.dbContext.InMemory;
using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;

namespace UnitTests.Fixture
{
    public class DBFixture
    {
        public dbContextFactory _dbContextFactory = new InMemoryDbContextFactory();
        private UserObjectMother userOM = new UserObjectMother();
        private ClubObjectMother clubOM = new ClubObjectMother();
        private LeagueObjectMother leagueOM = new LeagueObjectMother();
        private MatchObjectMother matchOM = new MatchObjectMother();
        public DBFixture()
        { 

        }
        public List<User> AddUsers(int cnt)
        {
            var users = new List<User>();
            for (int i = 0; i < cnt; i++)
            {
                users.Add(userOM.CreateUser(i + 1, $"nametest {i + 1}")
                                    .WithLogin($"logintest {i + 1}")
                                    .WithPassword($"passwordtest {i + 1}")
                                    .BuildCoreModel());

            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.users.AddRange(users);
            db_context.SaveChanges();
            return users;
        }
        public List<Club> AddClubs(int cnt)
        {
            var clubs = new List<Club>();
            for (int i = 0;i < cnt;i++)
            {
                clubs.Add(clubOM.CreateClub().WithId(i + 1).WithName($"nametest {i + 1}").BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.clubs.AddRange(clubs);
            db_context.SaveChanges();

            return clubs;
        }
        public List<League> AddLeagues(int cnt)
        {
            var leagues = new List<League>();
            for (int i = 0; i < cnt; i++)
            {
                leagues.Add(leagueOM.CreateLeague().WithId(i + 1).WithName($"nametest {i + 1}").BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.leagues.AddRange(leagues);
            db_context.SaveChanges();

            return leagues;
        }
        public List<Match> AddMatches(int cnt)
        {
            var matches = new List<Match>();
            for (int i = 0; i < cnt; i++)
            {
                matches.Add(matchOM.CreateMatch()
                                    .WithId(i + 1)
                                    .WithIdLeague(i + 1)
                                    .WithIdHome(i + 1)
                                    .WithIdGuest(i + 1)
                                    .WithGoalHome(i + 1)
                                    .WithGoalGuest(i + 1)
                                    .BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.matches.AddRange(matches);
            db_context.SaveChanges();

            return matches;
        }
    }
}
