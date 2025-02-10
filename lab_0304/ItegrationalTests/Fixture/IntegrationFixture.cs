using lab_01.DA.dbContext;
using lab_01.DA.dbContext.InMemory;
using lab_01.DA.dbContext.PostgreSQL;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_04.DA;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Match = lab_03.BL.Models.Match;

namespace ItegrationalTests.Fixture
{
    public class IntegrationFixture
    {
        public dbContextFactory _dbContextFactory = new pgSqlDbContextFactory("Server=postgres;Username=postgres;Password=123456789;Database=TestTestTest");
        //public dbContextFactory _dbContextFactory = new InMemoryDbContextFactory();
        public static bool SkipTest = Environment.GetEnvironmentVariable("skip") == "true";
        //public static string TestSkipTest = Environment.GetEnvironmentVariable("skip");
        protected UserObjectMother userOM = new UserObjectMother();
        protected ClubObjectMother clubOM = new ClubObjectMother();
        protected LeagueObjectMother leagueOM = new LeagueObjectMother();
        protected MatchObjectMother matchOM = new MatchObjectMother();

        //protected IMatchRepository _matchRepo;
        //protected IClubRepository _clubRepo;
        //protected IUserRepository _userRepo;
        //protected ILeagueRepository _leagueRepo;
        public IntegrationFixture()
        {
            // _dbContextFactory 
            //_matchRepo = new MatchRepository(_dbContextFactory, NullLogger<MatchRepository>.Instance);
            //_clubRepo = new ClubRepository(_dbContextFactory, NullLogger<ClubRepository>.Instance);
            //_userRepo = new UserRepository(_dbContextFactory, NullLogger<UserRepository>.Instance);
            //_leagueRepo = new LeagueRepository(_dbContextFactory, NullLogger<LeagueRepository>.Instance);
        }
        //~IntegrationFixture()
        //{
        //    _dbContextFactory.get_db_context().Database.EnsureDeleted();

        //}
        public List<User> AddUsers(int cnt = 10)
        {
            var users = new List<User>();
            for (int i = 0; i < cnt; i++)
            {
                users.Add(userOM.CreateUser(i + 1, $"nametest {i + 1}")
                                    .WithLogin($"logintest {i + 1}")
                                    .WithPassword($"passwordtest {i + 1}")
                                    .WithEmail($"email{i + 1}@mail.ru")
                                    .WithConfirmedEmail()
                                    .BuildCoreModel());

            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.users.AddRange(users);
            db_context.SaveChanges();
            return users;
        }
        public List<Club> AddClubs(int cnt = 10)
        {
            var clubs = new List<Club>();
            for (int i = 0; i < cnt; i++)
            {
                clubs.Add(clubOM.CreateClub().WithId(i + 1).WithIdLeague(1).WithName($"nametest {i + 1}").BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.clubs.AddRange(clubs);
            db_context.SaveChanges();

            return clubs;
        }
        public List<League> AddLeagues(int cnt = 10)
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
        public List<Match> AddMatches(int cnt = 10)
        {
            var matches = new List<Match>();
            for (int i = 0; i < cnt; i++)
            {
                matches.Add(matchOM.CreateMatch()
                                    .WithId(i + 1)
                                    .WithIdLeague(1)
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
        
        //public void Dispose()
        //{
        //    _dbContextFactory.get_db_context().Database.EnsureDeleted();
        //}
    }
}
