using lab_03.BL.IRepositories;
using lab_03.BL.Services;
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
    public class UserRepositoryUnitTests
    {
        private IUserRepository _userRepository;
        private DBFixture _dbFixture;
        private UserObjectMother _userObjectMother;
        public UserRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _userRepository = new UserRepository(_dbFixture._dbContextFactory, NullLogger<UserRepository>.Instance);
            _userObjectMother = new UserObjectMother();
        }
        [Fact]
        public void TestReadById()
        {
            var users = _dbFixture.AddUsers(10);
            var expected = users[0];

            var actual = _userRepository.readById(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestReadByLogin()
        {
            var users = _dbFixture.AddUsers(10);
            var expected = users.First();

            var actual = _userRepository.readByLogin(expected.Login);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestReadByRole()
        {
            var users = _dbFixture.AddUsers(10);
            var expected = users.Where(u => u.Role == "Referee").ToList();

            var actual = _userRepository.readByRole("Referee");

            Assert.Equal(expected.Count, actual.Count);
        }
        [Fact]
        public void TestDelete()
        {
            var users = _dbFixture.AddUsers(10);
            var user = users.First();

            _userRepository.delete(user);

            var actual = _userRepository.readByRole("Referee");
            Assert.Equal(9, actual.Count());
        }
        [Fact]
        public void TestUpdate()
        {
            var users = _dbFixture.AddUsers(10);
            var user = _userObjectMother.CreateUser(users.First().Id, "abc").WithLogin("testt").WithPassword("123").BuildCoreModel();

            _userRepository.update(user);

            var actual = _userRepository.readById(user.Id);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestCreate()
        {
            var users = _dbFixture.AddUsers(10);
            var user = _userObjectMother.CreateReferee().BuildCoreModel();

            _userRepository.create(user);

            var actual = _userRepository.readById(11);
            Assert.Equivalent(user, actual);
        }
    }
}
