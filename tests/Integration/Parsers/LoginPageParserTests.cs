using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeptunPro.Parsers;
using NeptunPro.Models;
using System;
using System.IO;

namespace NeptunPro.Tests.Integration.Deserializers
{
    [TestClass]
    public class LoginPageParserTests
    {
        [TestMethod]
        public void Can_Extract_Build_Number()
        {
            string sourceCode = File.ReadAllText(Path.Combine(Constants.ResourceFolder, "LoginPage.html"));

            var actual = LoginPageParser.BuildDetails(sourceCode);

            var expected = new NeptunBuildDetails(455, new DateTime(2018, 07, 19));

            Assert.AreEqual(expected, actual);
        }
    }
}
