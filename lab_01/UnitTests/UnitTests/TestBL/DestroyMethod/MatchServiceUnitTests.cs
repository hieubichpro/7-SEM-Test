using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;

namespace UnitTests.UnitTests.TestBL.DestroyMethod
{
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("Match Services Unit tests")]
    [AllureSubSuite("Match Service unit tests Destroit Method")]
    public class MatchServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private MatchObjectMother matchOM = new MatchObjectMother();
        private ClubObjectMother clubOM = new ClubObjectMother();
        [AllureBefore]
        public MatchServiceUnitTests()
        {

        }
        [Fact]
        public void TestGetNameClubByIDSuccess()
        {
            var matches = fixture.AddMatches(10);
            var clubs = fixture.AddClubs(10);
            var club = clubs.First();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            var actual = matchService.getNameClubById(matches.First().Id);

            Assert.Equal(club.Name, actual);
        }
        [Fact]
        public void TestGetNameClubByIDFailure()
        {
            var matches = fixture.AddMatches(10);
            var club = clubOM.CreateClub().WithId(matches.First().IdHomeTeam).WithName("clubtest").BuildCoreModel();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => matchService.getNameClubById(matches.First().IdHomeTeam));
        }
        [Fact]
        public void TestGetMatchByIdLeague()
        {
            var matches = fixture.AddMatches(10);
            var match = matches[0];
            var club = clubOM.CreateClub().WithId(match.IdHomeTeam).WithName("clubtest").BuildCoreModel();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            var actual = matchService.getMatchByIdLeague(matches[0].Id);

            Assert.Equivalent(matches, actual);
        }
        [Fact]
        public void TestEnterScoreSuccess()
        {
            var matches = fixture.AddMatches(10);
            var match = matches.First();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            matchService.EnterScore(match.Id, 4, 2);

            var actual = _matchRepo.readByID(match.Id);
            Assert.Equal(4, actual.GoalHomeTeam);
            Assert.Equal(2, actual.GoalGuestTeam);
        }
        [Fact]
        public void TestEnterScoreFailure()
        {
            var matches = fixture.AddMatches(10);
            var match = matchOM.CreateMatch().WithId(100).WithIdHome(1).WithGoalHome(2).WithGoalGuest(3).BuildCoreModel();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            Assert.Throws<MatchNotFoundException>(() => matchService.EnterScore(match.Id, match.GoalHomeTeam, match.GoalGuestTeam));
        }
        [Fact]
        public void TestGetByIdSuccess()
        {
            var matches = fixture.AddMatches(10);
            var match = matches.First();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            var actual = matchService.getById(match.Id);

            Assert.Equivalent(match, actual);
        }
        [Fact]
        public void TestGetByIdFailure()
        {
            var matches = fixture.AddMatches(10);
            var match = matchOM.CreateMatch().WithId(100).BuildCoreModel();
            IMatchRepository _matchRepo = new MatchRepository(fixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var matchService = new MatchService(_matchRepo, _clubRepo, NullLogger<MatchService>.Instance);

            Assert.Throws<MatchNotFoundException>(() => matchService.getById(match.Id));
        }
    }
}
