using NeptunPro.Tests.Regression.Models;

namespace NeptunPro.Tests.Regression
{
    public abstract class BaseRegressionTest
    {
        public static User User { get; private set; }



        public BaseRegressionTest()
        {
            string myValidNeptunUserName = "";
            string myValidNeptunPassword = "";

            User = new User(myValidNeptunUserName, myValidNeptunPassword);
        }
    }
}
