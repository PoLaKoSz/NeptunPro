using HtmlAgilityPack;
using NeptunPro.Models.XHR.Requests;

namespace NeptunPro.Deserializers
{
    public class HiddenFormDeserializer
    {
        public static void UpdateHiddenData(PostForm postForm, string sourceCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            postForm.EventValidation = GetHiddenFieldValue(doc, "__EVENTVALIDATION");
            postForm.ViewState = GetHiddenFieldValue(doc, "__VIEWSTATE");
            postForm.ViewStateGenerator = GetHiddenFieldValue(doc, "__VIEWSTATEGENERATOR");
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
