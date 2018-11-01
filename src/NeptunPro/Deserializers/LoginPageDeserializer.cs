using HtmlAgilityPack;
using NeptunPro.Models;
using System;
using System.Text.RegularExpressions;

namespace NeptunPro.Deserializers
{
    public class LoginPageDeserializer : Logger
    {
        public static NeptunBuildDetails BuildDetails(string sourceCode)
        {
            int outputVersion = 0;
            DateTime outputDate = new DateTime(1999, 12, 31);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            HtmlNode messageTableNode = doc.DocumentNode.SelectSingleNode("/html/body/form/table/tr[2]/td/table/tr/td[2]/div/table/tr[2]/td/span[@id=\"lblVersion\" and @class=\"szovegversion\"]");

            if (messageTableNode == null)
            {
                Log.Error("Couldn't find build version node!");
                return new NeptunBuildDetails(outputVersion, outputDate);
            }

            Regex regex = new Regex(@"Build: (?<buildNumber>\d+) \((?<buildDate>\d+.\d+.\d+.)\)");
            MatchCollection matchCollection = regex.Matches(messageTableNode.InnerText);

            try
            {
                outputVersion = int.Parse(matchCollection[0].Groups[1].Value);
                outputDate = DateTime.Parse(matchCollection[0].Groups[2].Value);
            }
            catch (Exception)
            {

            }

            return new NeptunBuildDetails(outputVersion, outputDate);
        }
    }
}
