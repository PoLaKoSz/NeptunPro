using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeptunPro.EndPoints;
using NeptunPro.Models;
using System;
using System.Threading.Tasks;

namespace NeptunPro.Tests.Regression.EndPoints
{
    [TestClass]
    public class LiveLoginPageTests : BaseRegressionTest
    {
        [TestMethod]
        public async Task Test_NeptunBuildNumber()
        {
            var loginPage = new LoginPage();

            var expected = new NeptunBuildDetails(456, new DateTime(2018, 11, 25));

            var actual = await loginPage.GetBuildDetails();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Test_GetMaxTryNumber_Endpoint()
        {
            var loginPage = new LoginPage();

            var actual = await loginPage.GetMaxTryNumber();

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.TryCount);
            Assert.IsTrue(0 <= actual.TryCount);
        }

        [TestMethod]
        public async Task Test_Valid_Login_Attemt()
        {
            var loginPage = new LoginPage();
            var loginCredentials = new LoginCredentials(User.UserName, User.Password);

            var loginResponse = await loginPage.Authenticate(loginCredentials);

            Assert.IsTrue(loginResponse.IsSuccess);
            Assert.AreEqual("Sikeres bejelentkezés", loginResponse.ErrorMessage);
            Assert.AreEqual("ok", loginResponse.ErrorCode);
            Assert.AreEqual("", loginResponse.WarningMessage);
        }

        [TestMethod]
        public async Task Test_InValid_Login_Attemt__Too_Short_Credentials()
        {
            var loginPage = new LoginPage();
            var loginCredentials = new LoginCredentials("", "");

            var loginResponse = await loginPage.Authenticate(loginCredentials);

            Assert.IsFalse(loginResponse.IsSuccess);
            Assert.AreEqual("Érvénytelen felhasználónév vagy jelszó.", loginResponse.ErrorMessage);
            Assert.AreEqual("accounterror", loginResponse.ErrorCode);
            Assert.AreEqual("", loginResponse.WarningMessage);
        }

        [TestMethod]
        public async Task Test_InValid_Login_Attemt__Wrong_Username_And_Or_Password()
        {
            var loginPage = new LoginPage();
            var loginCredentials = new LoginCredentials("BH6YG8", "dfgunrt7un5678n5678");

            var loginResponse = await loginPage.Authenticate(loginCredentials);

            Assert.IsFalse(loginResponse.IsSuccess);
            Assert.AreEqual("Hibás jelszó vagy azonosító!", loginResponse.ErrorMessage);
            Assert.AreEqual("accounterror", loginResponse.ErrorCode);
            Assert.AreEqual("", loginResponse.WarningMessage);
        }
    }
}
