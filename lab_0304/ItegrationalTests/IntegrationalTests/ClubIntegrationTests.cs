using Allure.Xunit.Attributes;
using ItegrationalTests.Fixture;
using lab_03.BL.IRepositories;
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
    [AllureOwner("Hieu Bauman")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("ClubIntegrational Tests")]
    public class ClubIntegrationTests
    {
        private ClubService _clubService;
        private IntegrationFixture _fixture = new IntegrationFixture();
        private ClubObjectMother clubOM = new ClubObjectMother();
        private IClubRepository _clubRepo;
        public ClubIntegrationTests()
        {
            _clubRepo = new ClubRepository(_fixture._dbContextFactory, NullLogger<ClubRepository>.Instance);
            _clubService = new ClubService(_clubRepo, NullLogger<ClubService>.Instance);
        }
        [SkippableFact]
        public void TestInsertClub()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var clubs = _fixture.AddClubs();
            var club = clubOM.CreateClub().WithName("testclubfunny").WithIdLeague(1).BuildCoreModel();
            int cnt = _clubService.getAll().Count;

            _clubService.insertClub(club.Name);

            Assert.Equal(cnt + 1, _clubService.getAll().Count);
        }
        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var clubs = _fixture.AddClubs();

            var actual = _clubService.getAll();

            Assert.Equal(clubs.Count, actual.Count);
        }
        [SkippableFact]
        public void TestModifyClub()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var clubs = _fixture.AddClubs();

            var club = clubOM.CreateClub().WithId(clubs.First().Id).WithName("modifysomeclub").BuildCoreModel();

            _clubService.modifyClub(club.Id, club.Name);

            var actual = _clubRepo.readbyId(club.Id);
            Assert.Equal(club.Name, actual.Name);
        }
    }
}
