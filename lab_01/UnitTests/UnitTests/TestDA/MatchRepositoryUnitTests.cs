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
    public class MatchRepositoryUnitTests
    {
        private IMatchRepository _matchRepository;
        private DBFixture _dbFixture;
        private MatchObjectMother _matchObjectMother;
        public MatchRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _matchRepository = new MatchRepository(_dbFixture._dbContextFactory, NullLogger<MatchRepository>.Instance);
            _matchObjectMother = new MatchObjectMother();
        }
        [Fact]
        public void TestCreate()
        {
            var matches = _dbFixture.AddMatches(10);
            var match = _matchObjectMother.CreateMatch().BuildCoreModel();

            _matchRepository.create(match);

            var actual = _matchRepository.readAll();
            Assert.Equal(11, actual.Count);
        }
        [Fact]
        public void TestUpdate()
        {
            var matches = _dbFixture.AddMatches(10);
            var match = _matchObjectMother.CreateMatch().WithId(matches.First().Id)
                .WithGoalHome(100).WithGoalGuest(100).BuildCoreModel();

            _matchRepository.update(match);

            var actual = _matchRepository.readByID(match.Id);
            Assert.Equivalent(match, actual);
        }
        [Fact]
        public void TestReadById()
        {
            var matches = _dbFixture.AddMatches(10);
            var match = matches.First();

            var actual = _matchRepository.readByID(match.Id);

            Assert.Equivalent(match, actual);
        }
        [Fact]
        public void TestReadAll()
        {
            var matches = _dbFixture.AddMatches(10);

            var actual = _matchRepository.readAll();

            Assert.Equivalent(matches, actual);
        }
    }
}
