using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Services;
using lab_04.DA;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;

namespace UnitTests.UnitTests.TestBL.DestroyMethod
{
    [AllureOwner("Hieu Bauman")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("ClubServices Unit tests")]
    [AllureSubSuite("ClubService unit tests Destroit Method")]
    public class ClubServiceUnitTests
    {
        private ClubObjectMother clubOM = new ClubObjectMother();
        private DBFixture fixture = new DBFixture();
        [AllureBefore]
        public ClubServiceUnitTests() { }
        //[Fact]
        //public void TestInsertClubSuccessDestroitMethod()
        //{
        //    var clubs = fixture.AddClubs(10);
        //    var club = clubOM.CreateClub().WithId(100).WithName("newname").BuildCoreModel();
        //    IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
        //    var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

        //    clubService.insertClub(club.Name);

        //    var actual = _clubRepo.readAll();
        //    Assert.Equal(11, clubs.Count);
        //}
        [Fact]
        public void TestInsertClubFailureDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubs.First();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubExistException>(() => clubService.insertClub(club.Name));
        }
        [Fact]
        public void TestGetIdClubByNameSuccessDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubs.First();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            var actual = clubService.getIdClubByName(club.Name);

            Assert.Equal(club.Id, actual);
        }
        [Fact]
        public void TestGetIdClubByNameFailureDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => clubService.getIdClubByName(club.Name));
        }
        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            var actual = clubService.getAll();

            Assert.Equivalent(clubs, actual);
        }
        [Fact]
        public void TestModifyClubSuccessDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubOM.CreateClub().WithId(clubs.First().Id).WithName("expected").BuildCoreModel();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            clubService.modifyClub(club.Id, club.Name);

            Assert.Equal("expected", club.Name);
        }
        [Fact]
        public void TestModifyClubFailureDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => clubService.modifyClub(club.Id, club.Name));
        }
        [Fact]
        public void TestDeleteClubSuccessDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            //var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            var club = clubs[0];
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            clubService.deleteClub(club);

            var actual = _clubRepo.readAll();
            Assert.Equal(9, actual.Count);
        }

        [Fact]
        public void TestDeleteClubFailureDestroitMethod()
        {
            var clubs = fixture.AddClubs(10);
            var club = clubOM.CreateClub().WithId(100).WithName("testclub").BuildCoreModel();
            IClubRepository _clubRepo = new ClubRepository(fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            var clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);

            Assert.Throws<ClubNotFoundException>(() => clubService.deleteClub(club));
        }
    }
}
