using NeptunPro.Deserializers;
using NeptunPro.Models.XHR.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeptunPro.DataAccessLayer.Web
{
    public abstract class EndPoint : IDisposable
    {
        private static readonly HttpClient _client;
        private static readonly PostForm _postForm;

        protected readonly Uri BaseAddress;



        static EndPoint()
        {
            var handler = new HttpClientHandler();

            _client = new HttpClient(handler, disposeHandler: true);
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:62.0) Gecko/20100101 Firefox/62.0");

            _postForm = new PostForm();
        }

        public EndPoint(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }



        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <exception cref="HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        protected async Task<string> GetAsync(Uri requestUri)
        {
            var response = await _client.GetAsync(requestUri);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="postForm">Form sent to the server.</param>
        /// <exception cref="HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        protected async Task<string> PostAsync(Uri requestUri, PostForm postForm)
        {
            postForm.UpdateWith(_postForm);

            string xhrForm = JsonConvert.SerializeObject(postForm);

            Dictionary<string, string> formVariables = JsonConvert.DeserializeObject<Dictionary<string, string>>(xhrForm);

            var formContent = new FormUrlEncodedContent(formVariables);

            string sourceCode = await PostAsync(requestUri, formContent);

            HiddenFormDeserializer.UpdateHiddenData(_postForm, sourceCode);

            return sourceCode;
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="httpContent">The HTTP request content sent to the server.</param>
        /// <exception cref="HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        protected async Task<string> PostAsync(Uri requestUri, HttpContent httpContent)
        {
            var response = await _client.PostAsync(requestUri, httpContent);

            string sourceCode = await response.Content.ReadAsStringAsync();

            return sourceCode;
        }


        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
