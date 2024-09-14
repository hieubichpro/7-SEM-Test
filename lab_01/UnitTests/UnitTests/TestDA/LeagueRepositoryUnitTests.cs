using lab_03.BL.IRepositories;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;

namespace UnitTests.UnitTests.TestDA
{
    public class LeagueRepositoryUnitTests
    {
        private DBFixture _dbFixture;
        private ILeagueRepository _leagueRepository;
        private LeagueObjectMother _leagueObjectMother;
        public LeagueRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _leagueRepository = new LeagueRepository(_dbFixture._dbContextFactory, NullLogger<LeagueRepository>.Instance);
            _leagueObjectMother = new LeagueObjectMother();
        }
        [Fact]
        public void TestReadByName()
        {
            var leagues = _dbFixture.AddLeagues(10);
            var league = leagues.First();

            var actual = _leagueRepository.readbyName(league.Name);

            Assert.Equivalent(league, actual);
        }
        [Fact]
        public void TestCreate()
        {
            var leagues = _dbFixture.AddLeagues(10);
            var league = _leagueObjectMother.CreateLeague().BuildCoreModel();

            _leagueRepository.create(league);

            var actual = _leagueRepository.readAll();
            Assert.Equal(11, actual.Count);
        }
        [Fact]
        public void TestReadById()
        {
            var leagues = _dbFixture.AddLeagues(10);
            var league = leagues.First();

            var actual = _leagueRepository.readById(league.Id);

            Assert.Equivalent(league, actual);
        }
        [Fact]
        public void TestReadAll()
        {
            var leagues = _dbFixture.AddLeagues(10);

            var actual = _leagueRepository.readAll();

            Assert.Equivalent(leagues, actual);
        }
        [Fact]
        public void TestReadByIdUser()
        {
            var leagues = _dbFixture.AddLeagues(10);
            var expected = leagues.Where(l => l.IdUser == 1);

            var actual = _leagueRepository.readByIdUser(1);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestUpdate()
        {
            var leagues = _dbFixture.AddLeagues(10);
            var league = _leagueObjectMother.CreateLeague().WithName("testleague").BuildCoreModel();

            _leagueRepository.update(league);

            var actual = _leagueRepository.readById(league.Id);
            Assert.Equivalent(league, actual);
        }
    }
}
