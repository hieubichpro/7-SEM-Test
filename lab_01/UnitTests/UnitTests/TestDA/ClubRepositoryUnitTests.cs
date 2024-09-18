using Allure.Xunit.Attributes;
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
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("DA Unit Tests")]
    [AllureSubSuite("ClubRepositoty Unit tests")]
    public class ClubRepositoryUnitTests
    {
        private IClubRepository _clubRepository;
        private DBFixture _dbFixture;
        private ClubObjectMother _clubObjectMother;
        public ClubRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _clubRepository = new ClubRepository(_dbFixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            _clubObjectMother = new ClubObjectMother();
        }
        [Fact]
        public void TestReadByName()
        {
            var clubs = _dbFixture.AddClubs(10);
            var club = clubs.First();

            var actual = _clubRepository.readbyName(club.Name);

            Assert.Equivalent(club, actual);
        }
        [Fact]
        public void TestReadById()
        {
            var clubs = _dbFixture.AddClubs(10);
            var club = clubs.First();

            var actual = _clubRepository.readbyId(club.Id);

            Assert.Equivalent(club, actual);
        }
        [Fact]
        public void TestCreate()
        {
            var clubs = _dbFixture.AddClubs(10);
            var club = _clubObjectMother.CreateClub().BuildCoreModel();

            _clubRepository.create(club);

            var actual = _clubRepository.readAll();
            Assert.Equal(11, actual.Count());
        }
        [Fact]
        public void TestReadAll()
        {
            var clubs = _dbFixture.AddClubs(10);

            var actual = _clubRepository.readAll();

            Assert.Equal(clubs.Count, actual.Count);
        }
        [Fact]
        public void TestUpdate()
        {
            var clubs = _dbFixture.AddClubs(10);
            var club = _clubObjectMother.CreateClub().WithId(clubs.First().Id)
                .WithName("abcbdh").BuildCoreModel();

            _clubRepository.update(club);

            var actual = _clubRepository.readbyId(club.Id);
            Assert.Equivalent(club, actual);
        }
        [Fact]
        public void TestDelete()
        {
            var clubs = _dbFixture.AddClubs(10);
            var club = clubs.First();

            _clubRepository.delete(club);

            var actual = _clubRepository.readAll();
            Assert.Equal(9, actual.Count);
        }
    }
}
