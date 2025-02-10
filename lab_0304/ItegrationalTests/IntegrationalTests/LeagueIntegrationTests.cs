using Allure.Xunit.Attributes;
using ItegrationalTests.Fixture;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;

namespace ItegrationalTests.IntegrationalTests
{
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("LeagueIntegrational Tests")]
    public class LeagueIntegrationTests
    {
        private LeagueService _leagueService;
        private IntegrationFixture _fixture = new IntegrationFixture();
        private LeagueObjectMother leagueOM = new LeagueObjectMother();
        public LeagueIntegrationTests()
        {
            var _leagueRepo = new LeagueRepository(_fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var _matchRepo = new MatchRepository(_fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            var _clubRepo = new ClubRepository(_fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);

            _leagueService = new LeagueService(_leagueRepo, _matchRepo, _clubRepo, NullLogger<LeagueService>.Instance);
        }
        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var leagues = _fixture.AddLeagues();

            var actual = _leagueService.getAll();

            Assert.Equal(leagues.Count, actual.Count);
        }
        [SkippableFact]
        public void TestModifyLeague()
        {
            Skip.If(IntegrationFixture.SkipTest);
            //int id = 1;
            var leagues = _fixture.AddLeagues();

            var league = leagueOM.CreateLeague().WithId(leagues.First().Id).WithName("Funnyyy").BuildCoreModel();

            _leagueService.modifyLeague(league.Id, league.Name, league.IdUser);

            var actual = _leagueService.getById(league.Id);
            Assert.Equal(league.Name, actual.Name);
            Assert.Equal(league.Id, actual.Id);
        }
        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var leagues = _fixture.AddLeagues();

            //int id = 1;
            var league = leagues.First();

            var actual = _leagueService.getById(league.Id);

            Assert.Equivalent(league, actual);
        }
    }
}
