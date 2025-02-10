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
    [AllureSubSuite("MatchIntegrational Tests")]
    public class MatchIntegrationTests
    {
        private MatchService _matchService;
        private IntegrationFixture _fixture = new IntegrationFixture();
        private MatchObjectMother  matchOM = new MatchObjectMother();
        public MatchIntegrationTests()
        {
            var _matchRepo = new MatchRepository(_fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            var _clubRepo = new ClubRepository(_fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            _matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);
        }
        [SkippableFact]
        public void TestGetNameClubById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var clubs = _fixture.AddClubs();
            var club = clubs.First();

            string actual = _matchService.getNameClubById(club.Id);

            Assert.Equal(club.Name, actual);
        }
        [SkippableFact]
        public void TestGetMatchByIdLeague()
        {
            Skip.If(IntegrationFixture.SkipTest);
            //int id = 1;
            var matches = _fixture.AddMatches();
            //var matches = _matchRepo.readByIdLeague(1);

            var actual = _matchService.getMatchByIdLeague(matches.First().IdLeague);

            Assert.Equal(matches.Count, actual.Count);
        }
        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var matches = _fixture.AddMatches();

            //int id = 6;
            var match = matches.First();

            var actual = _matchService.getById(match.Id);

            Assert.Equal(match.Id, actual.Id);
            Assert.Equal(match.GoalHomeTeam, actual.GoalHomeTeam);
            Assert.Equal(match.GoalGuestTeam, actual.GoalGuestTeam);
        }
        [SkippableFact]
        public void TestEnterScore()
        {
            Skip.If(IntegrationFixture.SkipTest);
            //Assert.Equal(match.Id, actual.Id);
            var matches = _fixture.AddMatches();
            var match = matchOM.CreateMatch().WithId(matches.First().Id).WithGoalHome(4).WithGoalGuest(5).BuildCoreModel();

            _matchService.EnterScore(match.Id, match.GoalHomeTeam, match.GoalGuestTeam);

            var actual = _matchService.getById(match.Id);
            Assert.Equivalent(match.GoalHomeTeam, actual.GoalHomeTeam);
            Assert.Equivalent(match.GoalGuestTeam, actual.GoalGuestTeam);
        }
    }
}
