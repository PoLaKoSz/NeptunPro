using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeptunPro.EndPoints;
using NeptunPro.Models;
using System.Threading.Tasks;

namespace NeptunPro.Tests.Regression.EndPoints
{
    [TestClass]
    public class LiveLoginPageTests : BaseRegressionTest
    {
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
        public async Task Valid_Login_Attemt__Should_Return_True()
        {
            var loginPage = new LoginPage();

            var loginCredentials = new LoginCredentials(User.UserName, User.Password);

            var actual = await loginPage.LogIn(loginCredentials);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task InValid_Login_Attemt__Should_Return_False()
        {
            var loginPage = new LoginPage();

            var loginCredentials = new LoginCredentials("", "");

            var actual = await loginPage.LogIn(loginCredentials);

            Assert.IsFalse(actual);
        }
    }
}
