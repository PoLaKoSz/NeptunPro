using HtmlAgilityPack;
using NeptunPro.Models;
using System;
using System.Collections.Generic;

namespace NeptunPro.Deserializers
{
    public static class MessagesPageDeserializer
    {
        public static List<Message> InBox(string sourceCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            var messageTableNode = doc.DocumentNode.SelectSingleNode("//div[@id=\"c_messages_gridMessages_grid_body_div\"]/table[@id=\"c_messages_gridMessages_bodytable\"]/tbody[@class=\"scrollablebody\"]");

            if (messageTableNode == null)
                return new List<Message>();

            return ExtractMessages(messageTableNode);
        }


        private static List<Message> ExtractMessages(HtmlNode messageTableNode)
        {
            var messages = new List<Message>();
            var messageNodes = messageTableNode.SelectNodes("./tr");

            if (messageNodes == null ||
                messageNodes[0].InnerText.Equals("Nincs találat"))
                return messages;

            foreach (var messageNode in messageNodes)
            {
                int id = ExtractID(messageNode);
                string sender = messageNode.SelectSingleNode("./td[5]").InnerText.Trim();
                string subject = messageNode.SelectSingleNode("./td[7]/span").InnerText.Trim();
                DateTime time = ExtractTime(messageNode);

                messages.Add(new Message(id, sender, subject, time));
            }

            return messages;
        }

        private static int ExtractID(HtmlNode messageNode)
        {
            string dirtyID = messageNode.GetAttributeValue("id", "tr__-1");

            string cleanedID = dirtyID.Substring(4, dirtyID.Length - 4);

            return Convert.ToInt32(cleanedID);
        }

        /// <summary>
        /// Get the UTC time when the message was sent
        /// </summary>
        private static DateTime ExtractTime(HtmlNode messageNode)
        {
            string stringTime = messageNode.SelectSingleNode("./td[8]").InnerText;

            DateTime parsedTime = DateTime.Parse(stringTime);

            return TimeAsUTC(parsedTime);
        }

        /// <summary>
        /// Change the given <see cref="DateTime"/> kind from local to UTC
        /// </summary>
        /// <returns>Time in UTC</returns>
        private static DateTime TimeAsUTC(DateTime time)
        {
            return time.AddHours(-2);
        }
    }
}
