using Allure.Xunit.Attributes;
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
[AllureSuite("League Services Unit tests")]
[AllureSubSuite("League Service unit tests London Method")]
    public class LeagueServiceUnitTests
    {
        private LeagueObjectMother leagueOM = new LeagueObjectMother();
        private ServiceFixture fixture = new ServiceFixture();
        public LeagueServiceUnitTests() { }
        //[Fact]
        //public void TestInsertLeagueSuccess()
        //{
        //    var leagues = fixture.PrepareLeaguesForTest();
        //    var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
        //    Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
        //    _leagueRepoMock.Setup(m => m.readbyName(league.Name)).Returns(leagues.FirstOrDefault(l => l.Name == league.Name));
        //    _leagueRepoMock.Setup(m => m.create(league)).Callback(() =>
        //    {
        //        leagues.Add(league);
        //    });
        //    var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

        //    leagueService.insertLeague(league.Name, league.IdUser);

        //    Assert.Equal(10, leagues.Count);
        //    _leagueRepoMock.Verify(m => m.create(league), Times.Once());
        //}
        [Fact]
        public void TestInsertLeagueFailure()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagues[0];
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readbyName(league.Name)).Returns(leagues.FirstOrDefault(l => l.Name == league.Name));

            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueExistException>(() => leagueService.insertLeague(league.Name, league.IdUser));
            _leagueRepoMock.Verify(m => m.readbyName(league.Name), Times.Once());
        }
        [Fact]
        public void TestDeleteLeagueSuccess()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagues[0];
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(league);
            _leagueRepoMock.Setup(m => m.delete(league)).Callback(() =>
            {
                leagues.Remove(league);
            });
            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

            leagueService.deleteLeague(league);

            Assert.Equal(9, leagues.Count);
            _leagueRepoMock.Verify(m => m.delete(league), Times.Once());
        }
        [Fact]
        public void TestDeleteLeagueFailure()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(leagues.FirstOrDefault(l => l.Id == league.Id));

            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueNotFoundException>(() => leagueService.deleteLeague(league));
            _leagueRepoMock.Verify(m => m.readById(league.Id), Times.Once());
            _leagueRepoMock.Verify(m => m.delete(league), Times.Never());
        }
        [Fact]
        public void TestGetAll()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readAll()).Returns(leagues);
            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

            var actual = leagueService.getAll();

            Assert.Equal(actual, leagues);
            _leagueRepoMock.Verify(m => m.readAll(), Times.Once());
        }
        [Fact]
        public void TestModifyLeagueSuccess()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagues[0];
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(league);
            _leagueRepoMock.Setup(m => m.update(league)).Callback(() =>
            {
                league.Name = "Newname";
                league.Id = 123;
            });
            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

            leagueService.modifyLeague(league.Id, league.Name, league.IdUser);

            Assert.Equivalent(league.Name, "Newname");
            Assert.Equal(123, league.Id);

            _leagueRepoMock.Verify(m => m.update(league), Times.Once());
        }
        [Fact]
        public void TestModifyLeagueFailure()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(leagues.FirstOrDefault(l => l.Id == league.Id));

            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueNotFoundException>(() => leagueService.modifyLeague(league.Id, league.Name, league.IdUser));
            _leagueRepoMock.Verify(m => m.readById(league.Id), Times.Once());
            _leagueRepoMock.Verify(m => m.update(league), Times.Never());
        }
        [Fact]
        public void TestGetByIdSuccess()
        {
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(league);

            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

            var actual = leagueService.getById(league.Id);

            Assert.Equal(league, actual);
            _leagueRepoMock.Verify(m => m.readById(league.Id), Times.Once());
        }
        [Fact]
        public void TestGetByIdFailure()
        {
            var leagues = fixture.PrepareLeaguesForTest();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            Mock<ILeagueRepository> _leagueRepoMock = new Mock<ILeagueRepository>();
            _leagueRepoMock.Setup(m => m.readById(league.Id)).Returns(leagues.FirstOrDefault(l => l.Id == league.Id));

            var leagueService = new LeagueService(_leagueRepoMock.Object, null, null, null, NullLogger<LeagueService>.Instance);

            Assert.Throws<LeagueNotFoundException>(() => leagueService.getById(league.Id));
            _leagueRepoMock.Verify(m => m.readById(league.Id), Times.Once());
        }
    }
}
