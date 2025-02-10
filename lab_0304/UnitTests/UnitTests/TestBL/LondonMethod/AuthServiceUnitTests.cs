using Allure.Xunit.Attributes;
using lab_01.BL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;

namespace UnitTests.UnitTests.TestBL.LondonMethod
{
    [AllureOwner("Hieu Bauman")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("AuthServices Unit tests")]
    [AllureSubSuite("AuthService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class AuthServiceUnitTests
    {
        private DBFixture _dbFixture = new();
        private UserObjectMother _userObjectMother = new();
        private  IOptions<ConfirmingEmailConfiguration> _confirmingEmailConfiguration;
        public AuthServiceUnitTests()
        {
            _confirmingEmailConfiguration = Options.Create(new ConfirmingEmailConfiguration()
            {
                Email = "xf21iu26@student.bmstu.ru",
                Password = "u7473xb3",
                CodeLifeTimeMinutes = 5
            });
        }
    }
}
