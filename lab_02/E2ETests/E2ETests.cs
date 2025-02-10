using Allure.Xunit.Attributes;
using lab_01.DA.dbContext;
using lab_01.DA.dbContext.PostgreSQL;
using lab_03.BL.IRepositories;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using UnitTests.ObjectMothers;
using Xunit;

namespace E2ETests
{
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("E2E Tests")]
    //[AllureSubSuite("ClubIntegrational Tests")]
    public class E2ETests
    {
        //private IConfiguration config;
        //private 
        private dbContextFactory _dbContextFactory = new pgSqlDbContextFactory("Server=localhost;Username=postgres;Password=123456789;Database=TestTestTest");
        public bool SkipTest = Environment.GetEnvironmentVariable("skip") == "true";

        private IUserRepository _userRepo;
        private IClubRepository _clubRepo;
        private ILeagueRepository _leagueRepo;
        private IMatchRepository _matchRepo;
        private UserService _userService;
        private LeagueService _leagueService;
        private MatchService _matchService;
        private ClubService _clubService;
        public E2ETests() 
        {
            _userRepo = new UserRepository(_dbContextFactory, NullLogger<UserRepository>.Instance);
            _clubRepo = new ClubRepository(_dbContextFactory, NullLogger<ClubRepository>.Instance);
            _leagueRepo = new LeagueRepository(_dbContextFactory, NullLogger<LeagueRepository>.Instance);
            _matchRepo = new MatchRepository(_dbContextFactory, NullLogger<MatchRepository>.Instance);
            _userService = new UserService(_userRepo, NullLogger<UserService>.Instance);
            _clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);
            _leagueService = new LeagueService(_leagueRepo, _matchRepo, _clubRepo, NullLogger<LeagueService>.Instance);
            _matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);
        }

        [SkippableFact]
        public void TestReferee()
        {
            Skip.If(SkipTest);
            var userOM = new UserObjectMother();
            var leagueOM = new LeagueObjectMother();
            var clubOM = new ClubObjectMother();
            var matchOM = new MatchObjectMother();
            var user = userOM.CreateReferee().WithLogin("teste2e").WithPassword("teste2e").WithPassword("teste2e").BuildCoreModel();
            int cnt = _userRepo.readAll().Count;

            _userService.Register(user.Login, user.Password, user.Role, user.Name);

            Assert.Equal(cnt + 1, _userRepo.readAll().Count);

            var actual = _userService.Login(user.Login, user.Password);

            Assert.Equal(user.Login, actual.Login);
            Assert.Equal(user.Password, actual.Password);
            Assert.Equal(user.Role, actual.Role);
            Assert.Equal(user.Name, actual.Name);

            var league = leagueOM.CreateLeague().WithName("teste2eleague").WithIdUser(actual.Id).BuildCoreModel();
            int cntLeague = _leagueRepo.readAll().Count;

            _leagueService.insertLeague(league.Name, league.IdUser);

            Assert.Equal(cntLeague + 1, _leagueRepo.readAll().Count());

            var club1 = clubOM.CreateClub().WithName("teste2eclub1").WithIdLeague(league.Id).BuildCoreModel();
            int cntClub1 = _clubRepo.readAll().Count;

            _clubService.insertClub(club1.Name);

            Assert.Equal(cntClub1 + 1, _clubRepo.readAll().Count);

            var club2 = clubOM.CreateClub().WithName("teste2eclub2").WithIdLeague(league.Id).BuildCoreModel();
            int cntClub2 = _clubRepo.readAll().Count;

            _clubService.insertClub(club2.Name);

            Assert.Equal(cntClub2 + 1, _clubRepo.readAll().Count);

            int cntMatch = _matchRepo.readAll().Count;

            _leagueService.Schedule(-1);

            Assert.Equal(cntMatch + 2, _matchRepo.readAll().Count);


            int cntLeague1 = _leagueRepo.readAll().Count;

            _leagueService.deleteLeague(league);

            Assert.Equal(cntLeague1 - 1, _leagueRepo.readAll().Count);
        }
    }
}