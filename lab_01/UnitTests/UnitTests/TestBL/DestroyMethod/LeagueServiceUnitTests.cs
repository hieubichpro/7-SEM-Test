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
using Allure.Xunit;
using Xunit;
using lab_04.DA;

namespace UnitTests.UnitTests.TestBL.DestroyMethod
{
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("League Services Unit tests")]
    //[AllureSubSuite("League Service unit tests London Method")]
    public class LeagueServiceUnitTests
    {
        private LeagueObjectMother leagueOM = new LeagueObjectMother();
        private DBFixture fixture = new DBFixture();
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
        public void TestInsertLeagueFailureDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagues[0];
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueExistException>(() => leagueService.insertLeague(league.Name, league.IdUser));
        }
        [Fact]
        public void TestDeleteLeagueSuccessDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagues[0];
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);

            leagueService.deleteLeague(league);

            var actual = _leagueRepo.readAll();
            Assert.Equal(9, actual.Count);
        }
        
        [Fact]
        public void TestDeleteLeagueFailureDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueNotFoundException>(() => leagueService.deleteLeague(league));
        }
        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);

            var actual = leagueService.getAll();

            Assert.Equivalent(actual, leagues);
        }
        [Fact]
        public void TestModifyLeagueSuccessDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagueOM.CreateLeague().WithId(leagues.First().Id).WithName("abcd").BuildCoreModel();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);

            leagueService.modifyLeague(league.Id, league.Name, league.IdUser);

            Assert.Equivalent(league.Name, "abcd");
        }
        [Fact]
        public void TestModifyLeagueFailureDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);


            Assert.Throws<LeagueNotFoundException>(() => leagueService.modifyLeague(league.Id, league.Name, league.IdUser));
        }
        [Fact]
        public void TestGetByIdSuccessDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagues.First();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);

            var actual = leagueService.getById(league.Id);

            Assert.Equivalent(league, actual);
        }
        [Fact]
        public void TestGetByIdFailureDestroitMethod()
        {
            var leagues = fixture.AddLeagues();
            var league = leagueOM.CreateLeague().WithId(100).WithName("aaa").BuildCoreModel();
            ILeagueRepository _leagueRepo = new LeagueRepository(fixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            var leagueService = new LeagueService(_leagueRepo, null, null, null, NullLogger<LeagueService>.Instance);

            Assert.Throws<LeagueNotFoundException>(() => leagueService.getById(league.Id));
        }
    }
}