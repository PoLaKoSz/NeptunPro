using HtmlAgilityPack;
using NeptunPro.Models.XHR.Requests;

namespace NeptunPro.Parsers
{
    internal class HiddenFormParser : Logger
    {
        internal static void FromWholePage(PostForm postForm, string sourceCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            postForm.EventValidation = GetHiddenFieldValue(doc, "__EVENTVALIDATION");
            postForm.ViewState = GetHiddenFieldValue(doc, "__VIEWSTATE");
            postForm.ViewStateGenerator = GetHiddenFieldValue(doc, "__VIEWSTATEGENERATOR");
        }

        internal static void FromApiResponse(PostForm postForm, string sourceCode)
        {

        }


        private static string GetHiddenFieldValue(HtmlDocument document, string name)
        {
            HtmlNode fieldNode = document.DocumentNode.SelectSingleNode("//input[@type=\"hidden\"][@name=\"" + name + "\"]");

            if (fieldNode != null)
            {
                return fieldNode.GetAttributeValue("value", "");
            }

            return "";
        }
    }
}
