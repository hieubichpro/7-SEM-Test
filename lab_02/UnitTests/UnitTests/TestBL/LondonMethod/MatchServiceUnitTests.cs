using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.BL.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;

namespace UnitTests.UnitTests.TestBL
{
    [AllureOwner("Hieu Bauman")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("MatchServices Unit tests")]
    [AllureSubSuite("MatchService unit tests London Method")]
    public class MatchServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private MatchObjectMother matchOM = new MatchObjectMother();
        private ClubObjectMother clubOM = new ClubObjectMother();
        [AllureBefore]
        public MatchServiceUnitTests()
        {

        }
        [Fact]
        public void TestGetNameClubByIDSuccess()
        {
            var matches = fixture.PrepareMatchesForTest();
            var match = matches[0];
            var club = clubOM.CreateClub().WithId(match.IdHomeTeam).WithName("clubtest").BuildCoreModel();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyId(match.IdHomeTeam)).Returns(club);
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            var actual = matchService.getNameClubById(match.Id);

            Assert.Equal(club.Name, actual);
            _clubRepoMock.Verify(m => m.readbyId(match.IdHomeTeam), Times.Once());
        }
        [Fact]
        public void TestGetNameClubByIDFailure()
        {
            var match = matchOM.CreateMatch().WithIdHome(100).BuildCoreModel();
            var clubs = fixture.PrepareClubsForTest();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyId(match.IdHomeTeam)).Returns(clubs.FirstOrDefault(c => c.Id == match.IdHomeTeam));
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => matchService.getNameClubById(match.Id));
        }
        [Fact]
        public void TestGetMatchByIdLeague()
        {
            var matches = fixture.PrepareMatchesForTest();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _matchRepoMock.Setup(m => m.readByIdLeague(matches[0].IdLeague)).Returns(matches);
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            var actual = matchService.getMatchByIdLeague(matches[0].Id);

            Assert.Equal(matches, actual);
            _matchRepoMock.Verify(m => m.readByIdLeague(matches[0].Id), Times.Once());
        }
        [Fact]
        public void TestEnterScoreSuccess()
        {
            var match = matchOM.CreateMatch().WithId(1).WithGoalHome(2).WithGoalGuest(3).BuildCoreModel();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _matchRepoMock.Setup(m => m.readByID(match.Id)).Returns(match);
            _matchRepoMock.Setup(m => m.update(match)).Callback(() =>
            {
                match.GoalHomeTeam = 1;
                match.GoalGuestTeam = 1;
            });
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            matchService.EnterScore(match.Id, match.GoalHomeTeam, match.GoalGuestTeam);

            Assert.Equal(1, match.GoalHomeTeam);
            Assert.Equal(1, match.GoalGuestTeam);
            _matchRepoMock.Verify(m => m.update(match), Times.Once());
        }
        [Fact]
        public void TestEnterScoreFailure()
        {
            var matches = fixture.PrepareMatchesForTest();
            var match = matchOM.CreateMatch().WithId(100).WithIdHome(1).WithGoalHome(2).WithGoalGuest(3).BuildCoreModel();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _matchRepoMock.Setup(m => m.readByID(match.Id)).Returns(matches.FirstOrDefault(m => m.Id == match.Id));
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            Assert.Throws<MatchNotFoundException>(() => matchService.EnterScore(match.Id, match.GoalHomeTeam, match.GoalGuestTeam));
            _matchRepoMock.Verify(m => m.readByID(match.Id), Times.Once());
        }
        [Fact]
        public void TestGetByIdSuccess()
        {
            var match = matchOM.CreateMatch().WithId(1).WithGoalHome(2).WithGoalGuest(3).BuildCoreModel();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _matchRepoMock.Setup(m => m.readByID(match.Id)).Returns(match);
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            var actual = matchService.getById(match.Id);

            Assert.Equal(match, actual);
            _matchRepoMock.Verify(m => m.readByID(match.Id), Times.Once());
        }
        [Fact]
        public void TestGetByIdFailure()
        {
            var matches = fixture.PrepareMatchesForTest();
            var match = matchOM.CreateMatch().WithId(100).BuildCoreModel();
            Mock<IMatchRepository> _matchRepoMock = new Mock<IMatchRepository>();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _matchRepoMock.Setup(m => m.readByID(match.Id)).Returns(matches.FirstOrDefault(m => m.Id == match.Id));
            var matchService = new MatchService(_matchRepoMock.Object, _clubRepoMock.Object, NullLogger<MatchService>.Instance);

            Assert.Throws<MatchNotFoundException>(() => matchService.getById(match.Id));
        }
    }
}