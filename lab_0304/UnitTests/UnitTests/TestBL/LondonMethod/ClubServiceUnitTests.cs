using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
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
    [AllureSuite("ClubServices Unit tests")]
[AllureSubSuite("ClubService unit tests London Method")]
    public class ClubServiceUnitTests
    {
        private ClubObjectMother clubOM = new ClubObjectMother();
        private ServiceFixture fixture = new ServiceFixture();
        [AllureBefore]
        public ClubServiceUnitTests() { }
        //[Fact]
        //public void TestInsertClubSuccess()
        //{
        //    var clubs = fixture.PrepareClubsForTest();
        //    var club = clubOM.CreateClub().WithId(100).BuildCoreModel();
        //    Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
        //    _clubRepoMock.Setup(m => m.create(club)).Callback(() => clubs.Add(club));
        //    var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

        //    clubService.insertClub(club.Name);

        //    Assert.Equal(11, clubs.Count);
        //    _clubRepoMock.Verify(m => m.create(club), Times.Once());
        //}
        //[Fact]
        //public void TestInsertClubFailure()
        //{
        //    var clubs = fixture.PrepareClubsForTest();
        //    var club = clubOM.CreateClub().WithId(1).BuildCoreModel();
        //    Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
        //    _clubRepoMock.Setup(m => m.create(club)).Callback(() => clubs.Add(club));
        //    var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

        //    Assert.Throws<ClubExistException>(() => clubService.insertClub(club.Name));
        //}
        [Fact]
        public void TestGetIdClubByNameSuccess()
        {
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyName(club.Name)).Returns(club);
            var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

            var actual = clubService.getIdClubByName(club.Name);

            Assert.Equal(club.Id, actual);
            _clubRepoMock.Verify(m => m.readbyName(club.Name), Times.Once());
        }
        [Fact]
        public void TestGetIdClubByNameFailure()
        {
            var clubs = fixture.PrepareClubsForTest();
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyName(club.Name)).Returns(clubs.FirstOrDefault(m => m.Name == club.Name));
            var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => clubService.getIdClubByName(club.Name));
            _clubRepoMock.Verify(m => m.readbyName(club.Name), Times.Once());
        }
        [Fact]
        public void TestGetAll()
        {
            var clubs = fixture.PrepareClubsForTest();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readAll()).Returns(clubs);
            var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

            var actual = clubService.getAll();

            Assert.Equal(clubs, actual);
            _clubRepoMock.Verify(m => m.readAll(), Times.Once());
        }
        [Fact]
        public void TestModifyClubSuccess()
        {
            var club = clubOM.CreateClub().BuildCoreModel();
            Mock<IClubRepository> _clubMockRepo = new Mock<IClubRepository>();
            _clubMockRepo.Setup(m => m.readbyId(club.Id)).Returns(club);
            _clubMockRepo.Setup(m => m.update(club)).Callback(() => 
            {
                club.Name = "test123";
                club.Id = 150;
            });
            var clubService = new ClubService(_clubMockRepo.Object, NullLogger<ClubService>.Instance);
            clubService.modifyClub(club.Id, club.Name);

            Assert.Equal("test123", club.Name);
            Assert.Equal(150, club.Id);
            _clubMockRepo.Verify(m => m.update(club), Times.Once());
        }
        //[Fact]
        //public void TestModifyClubFailure()
        //{
        //    var clubs = fixture.PrepareClubsForTest();
        //    var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
        //    Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
        //    _clubRepoMock.Setup(m => m.readbyId(club.Id)).Returns(clubs.FirstOrDefault(m => m.Id == club.Id));
        //    _clubRepoMock.Setup(m => m.readbyName(club.Name)).Returns(clubs.FirstOrDefault(m => m.Name == club.Name));
        //    var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

        //    Assert.Throws<ClubNotFoundException>(() => clubService.modifyClub(club.Id, club.Name));
        //    _clubRepoMock.Verify(m => m.readbyName(club.Name), Times.Once());
        //}
        [Fact]
        public void TestDeleteClubSuccess()
        {
            var clubs = fixture.PrepareClubsForTest();
            //var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            var club = clubs[0];
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyId(club.Id)).Returns(clubs.FirstOrDefault(m => m.Id == club.Id));
            _clubRepoMock.Setup(m => m.delete(club)).Callback(() => clubs.Remove(club));
            var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

            clubService.deleteClub(club);

            Assert.Equal(9, clubs.Count);
            _clubRepoMock.Verify(m => m.delete(club), Times.Once());
        }

        [Fact]
        public void TestDeleteClubFailure()
        {
            var clubs = fixture.PrepareClubsForTest();
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            Mock<IClubRepository> _clubRepoMock = new Mock<IClubRepository>();
            _clubRepoMock.Setup(m => m.readbyId(club.Id)).Returns(clubs.FirstOrDefault(m => m.Id == club.Id));
            _clubRepoMock.Setup(m => m.delete(club)).Callback(() => clubs.Remove(club));
            var clubService = new ClubService(_clubRepoMock.Object, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => clubService.deleteClub(club));
            _clubRepoMock.Verify(m => m.readbyId(club.Id), Times.Once());
        }
    }
}
