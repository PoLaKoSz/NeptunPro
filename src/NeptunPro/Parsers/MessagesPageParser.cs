using HtmlAgilityPack;
using NeptunPro.Models;
using System;
using System.Collections.Generic;

namespace NeptunPro.Parsers
{
    public class MessagesPageParser : Logger
    {
        /// <summary>
        /// Extract <see cref="Message"/> objects from the given source code (from the InBox).
        /// </summary>
        /// <exception cref="NodeNotFoundException">Webpage structure changed</exception>
        /// <exception cref="NodeNotFoundException">Data format changed on the Webpage</exception>
        public static List<Message> InBox(string sourceCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            var messageTableNode = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/form[1]/fieldset[1]/table[2]/tr[1]/td[3]/table[1]/tr[5]/td[2]/div[1]/div[1]/div[2]/div[1]/div[1][@id=\"c_messages_gridMessages_gridmaindiv\"]/ div[3][@id=\"c_messages_gridMessages_grid_body_div\"]/table[1][@id=\"c_messages_gridMessages_bodytable\"]/tbody[@class=\"scrollablebody\"]");

            if (messageTableNode == null)
            {
                Log.Info("Couldn't find the table node which should contains the messages!");
                return new List<Message>();
            }

            return ExtractMessages(messageTableNode);
        }

        /// <summary>
        /// Update the paramter <see cref="Message"/> with the message body
        /// </summary>
        /// <exception cref="NodeNotFoundException">Webpage structure changed</exception>
        public static void Ajax(string sourceCode, Message message)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceCode);

            HtmlNode messageBodyNode = doc.DocumentNode.SelectSingleNode("/div[5]/div[1]/div[1]/div[1]/div[1]/div[3]/span[1]/html[1]/body[1]");

            if (messageBodyNode == null)
            {
                Log.Error("Detailed message from Ajax call breaked the parser!");
                throw new NodeNotFoundException("Detailed message breaked the parser.");
            }

            try
            {
                foreach (var paragraph in messageBodyNode.SelectNodes(".//p"))
                {
                    message.Text += paragraph.InnerText + "\n";
                }

                message.Text = message.Text.Substring(0, message.Text.Length - 1);
            }
            catch (Exception)
            {
                Log.Warn("Detailed message body from Ajax call {body} is empty!", messageBodyNode.InnerHtml);
            }
        }


        private static List<Message> ExtractMessages(HtmlNode messageTableNode)
        {
            var messages = new List<Message>();
            var messageNodes = messageTableNode.SelectNodes("./tr");

            if (messageNodes != null
                && messageNodes[0].InnerText.Equals("Nincs találat"))
                return messages;

            if (messageNodes == null)
            {
                Log.Error("Couldn't find the tr node which should contains the messages!");
                throw new NodeNotFoundException("Couldn't find the tr node which should contains the messages.");
            }

            foreach (var messageNode in messageNodes)
            {
                try
                {
                    int id = ExtractID(messageNode);
                    string sender = messageNode.SelectSingleNode("./td[5]").InnerText.Trim();
                    string subject = messageNode.SelectSingleNode("./td[7]/span").InnerText.Trim();
                    DateTime time = ExtractTime(messageNode);

                    messages.Add(new Message(id, sender, subject, time));
                }
                catch (NullReferenceException ex)
                {
                    Log.Error(ex, "Undetailed message HTML structure changed to {messageNode} so could be parsed!", messageNode.InnerHtml);
                    throw new NodeNotFoundException("Couldn't parse undetailed message into object.", ex);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Couldn't parse undetailed message {messageNode} into object!", messageNode.InnerHtml);
                    throw new FormatException("Couldn't parse undetailed message into object.", ex);
                }
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
