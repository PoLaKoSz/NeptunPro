using HtmlAgilityPack;
using NeptunPro.Models;
using System;
using System.Collections.Generic;

namespace NeptunPro.Deserializers
{
    public static class MessagesPageDeserializer
    {
        /// <summary>
        /// Extract <see cref="Message"/> objects from the given source code (from the InBox)
        /// </summary>
        public static List<Message> InBox(string sourceCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            var messageTableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/form[1]/fieldset[1]/table[2]/tr[1]/td[3]/table[1]/tr[5]/td[2]/div[1]/div[1]/div[2]/div[1]/div[1][@id=\"c_messages_gridMessages_gridmaindiv\"]/ div[3][@id=\"c_messages_gridMessages_grid_body_div\"]/table[1][@id=\"c_messages_gridMessages_bodytable\"]/tbody[1][@class=\"scrollablebody\"]");

            if (messageTableNode == null)
                return new List<Message>();

            return ExtractMessages(messageTableNode);
        }

        /// <summary>
        /// Update the paramter <see cref="Message"/> with the message body
        /// </summary>
        public static void Api(string sourceCode, Message message)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            HtmlNode messageBodyNode = doc.DocumentNode.SelectSingleNode("/div[5]/div[1]/div[1]/div[1]/div[1]/div[3]/span[1]/html[1]/body[1]");

            if (messageBodyNode != null)
            {
                foreach (var paragraph in messageBodyNode.SelectNodes(".//p"))
                {
                    message.Text += paragraph.InnerText + "\n";
                }
            }

            message.Text = message.Text.Substring(0, message.Text.Length - 1);
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
