using NeptunPro.DataAccessLayer.Web;
using NeptunPro.Deserializers;
using NeptunPro.Models;
using NeptunPro.Models.XHR.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeptunPro.EndPoints
{
    public class MessagesPage : SecureEndPoint
    {
        public MessagesPage()
            : base(new Uri("https://neptun.uni-obuda.hu/hallgato/main.aspx")) { }



        /// <summary>
        /// Load the <see cref="Message"/>s from the InBox first page
        /// </summary>
        /// <returns>Collection of <see cref="Message"/> object which only has an ID, Sender, Subject and Title property</returns>
        public async Task<List<Message>> Load()
        {
            //string _sourceCode = await base.GetAsync(base.BaseAddress);

            //System.IO.File.WriteAllText("saved.html", _sourceCode, System.Text.Encoding.UTF8);

            string _sourceCode = System.IO.File.ReadAllText("saved.html");

            return MessagesPageDeserializer.InBox(_sourceCode);
        }

        /// <summary>
        /// Get more details for the parameter <see cref="Message"/>
        /// </summary>
        /// <returns><see cref="Message"/> with the filled Text property</returns>
        public async Task<Message> GetMessage(Message message)
        {
            var xhrMessageModel = new PostMessageForm();
            xhrMessageModel.SetID(message);

            //var _sourceCode = await base.PostAsync(base.BaseAddress, xhrMessageModel);

            //System.IO.File.WriteAllText("message_api.html", _sourceCode, System.Text.Encoding.UTF8);

            string _sourceCode = System.IO.File.ReadAllText("saved.html");

            return message;
        }
    }
}
