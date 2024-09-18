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
    public class MatchIntegrationTests : IntegrationFixture
    {
        private MatchService _matchService;
        public MatchIntegrationTests()
        {
            _matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);
        }
        [SkippableFact]
        public void TestGetNameClubById()
        {
            Skip.If(SkipTest);
            int id = 1;
            var club = _clubRepo.readbyId(id);

            string actual = _matchService.getNameClubById(id);

            Assert.Equal(club.Name, actual);
        }
        [SkippableFact]
        public void TestGetMatchByIdLeague()
        {
            Skip.If(SkipTest);
            int id = 1;
            var matches = _matchRepo.readByIdLeague(1);

            var actual = _matchService.getMatchByIdLeague(id);

            Assert.Equal(matches.Count, actual.Count);
        }
        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(SkipTest);
            int id = 6;
            var match = _matchRepo.readByID(id);

            var actual = _matchService.getById(id);

            Assert.Equal(match.Id, actual.Id);
        }
        [SkippableFact]
        public void TestEnterScore()
        {
            Skip.If(SkipTest);
            int id = 7;
            var match = matchOM.CreateMatch().WithId(id).WithGoalHome(4).WithGoalGuest(5).BuildCoreModel();

            _matchService.EnterScore(match.Id, match.GoalHomeTeam, match.GoalGuestTeam);

            var actual = _matchRepo.readByID(7);
            Assert.Equivalent(match, actual);
        }
    }
}
