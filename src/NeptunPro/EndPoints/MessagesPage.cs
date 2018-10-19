using NeptunPro.DataAccessLayer.Web;
using NeptunPro.Deserializers;
using NeptunPro.Models;
using NeptunPro.Models.XHR.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeptunPro.EndPoints
{
    public class MessagesPage : EndPoint
    {
        private string _sourceCode;



        public MessagesPage()
            : base(new Uri("https://neptun.uni-obuda.hu/hallgato/main.aspx"))
        {
            _sourceCode = "";
        }



        /// <summary>
        /// Load the <see cref="Message"/>s from the InBox first page
        /// </summary>
        /// <returns>Collection of <see cref="Message"/> object which only has an ID, Sender, Subject and Title property</returns>
        public async Task<List<Message>> Load()
        {
            var response = await _client.GetAsync(base.BaseAddress);

            _sourceCode = await response.Content.ReadAsStringAsync();

            System.IO.File.WriteAllText("saved.html", _sourceCode, System.Text.Encoding.UTF8);

            //_sourceCode = System.IO.File.ReadAllText("saved.html");

            return MessagesPageDeserializer.InBox(_sourceCode);
        }

        /// <summary>
        /// Get more details for the parameter <see cref="Message"/>
        /// </summary>
        /// <returns><see cref="Message"/> with the Text property</returns>
        public async Task<Message> GetMessage(Message message)
        {
            var xhrMessageModel = new PostMessageForm();
            xhrMessageModel.SetID(message);

            MessagesPageDeserializer.GetHiddenData(xhrMessageModel, _sourceCode);

            string xhrForm = JsonConvert.SerializeObject(xhrMessageModel);

            Dictionary<string, string> formVariables = JsonConvert.DeserializeObject<Dictionary<string, string>>(xhrForm);

            var formContent = new FormUrlEncodedContent(formVariables);

            var response = await _client.PostAsync(base.BaseAddress, formContent);

            _sourceCode = await response.Content.ReadAsStringAsync();

            System.IO.File.WriteAllText("message_api.html", _sourceCode, System.Text.Encoding.UTF8);

            return message;
        }
    }
}
