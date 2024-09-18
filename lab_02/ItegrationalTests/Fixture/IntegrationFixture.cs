using lab_01.DA.dbContext;
using lab_01.DA.dbContext.PostgreSQL;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_04.DA;
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
        private IConfiguration config;
        public dbContextFactory _dbContextFactory;
        public bool SkipTest = Environment.GetEnvironmentVariable("skip") == "true";
        protected UserObjectMother userOM = new UserObjectMother();
        protected ClubObjectMother clubOM = new ClubObjectMother();
        protected LeagueObjectMother leagueOM = new LeagueObjectMother();
        protected MatchObjectMother matchOM = new MatchObjectMother();

        protected IMatchRepository _matchRepo;
        protected IClubRepository _clubRepo;
        protected IUserRepository _userRepo;
        protected ILeagueRepository _leagueRepo;
        public IntegrationFixture()
        {
            config = new ConfigurationBuilder()
                                .AddJsonFile("dbsettings.json")
                                .Build();
            _dbContextFactory = new pgSqlDbContextFactory(config);
            _matchRepo = new MatchRepository(_dbContextFactory, NullLogger<MatchRepository>.Instance);
            _clubRepo = new ClubRepository(_dbContextFactory, NullLogger<ClubRepository>.Instance);
            _userRepo = new UserRepository(_dbContextFactory, NullLogger<UserRepository>.Instance);
            _leagueRepo = new LeagueRepository(_dbContextFactory, NullLogger<LeagueRepository>.Instance);
        }
    }
}
