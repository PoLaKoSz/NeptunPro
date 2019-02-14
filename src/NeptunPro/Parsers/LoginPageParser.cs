using HtmlAgilityPack;
using NeptunPro.Models;
using System;
using System.Text.RegularExpressions;

namespace NeptunPro.Parsers
{
    public class LoginPageParser : Logger
    {
        /// <summary>
        /// Extract Build details from the LoginPage
        /// </summary>
        /// <param name="sourceCode">Full source code of the LoginPage</param>
        /// <exception cref="NodeNotFoundException">Webpage structure changed</exception>
        /// <exception cref="NodeNotFoundException">Data format changed on the Webpage</exception>
        public static NeptunBuildDetails BuildDetails(string sourceCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            HtmlNode buildVersionNode = doc.DocumentNode.SelectSingleNode("/html/body/form/table/tr[2]/td/table/tr/td[2]/div/table/tr[2]/td/span[@id=\"lblVersion\" and @class=\"szovegversion\"]");

            if (buildVersionNode == null)
            {
                Log.Error("Couldn't find build version node!");
                throw new NodeNotFoundException("Couldn't find build version node");
            }

            return ExtractBuildVersion(buildVersionNode.InnerText);
        }


        /// <exception cref="NodeNotFoundException">Given string can't be parsed</exception>
        private static NeptunBuildDetails ExtractBuildVersion(string text)
        {
            Regex regex = new Regex(@"Build: (?<buildNumber>\d+) \((?<buildDate>\d+.\d+.\d+.)\)");
            MatchCollection matchCollection = regex.Matches(text);

            try
            {
                return new NeptunBuildDetails(
                    int.Parse(matchCollection[0].Groups[1].Value),
                    DateTime.Parse(matchCollection[0].Groups[2].Value));
            }
            catch (Exception ex)
            {
                Log.Error("Couldn't extract Neptun build details from {text}!", text);
                throw new FormatException("Neptun build details is not in the correct format", ex);
            }
        }
    }
}
