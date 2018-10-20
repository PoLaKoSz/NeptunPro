using NeptunPro.Models.XHR.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeptunPro.DataAccessLayer.Web
{
    /// <summary>
    /// Simple web access layer only for the bare minimum. Most EndPoints should use
    /// <see cref="SecureEndPoint"/> instead.
    /// </summary>
    public abstract class EndPoint : IDisposable
    {
        private static readonly HttpClient _client;

        protected readonly Uri BaseAddress;



        static EndPoint()
        {
            var handler = new HttpClientHandler();

            _client = new HttpClient(handler, disposeHandler: true);
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:62.0) Gecko/20100101 Firefox/62.0");
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
            string xhrForm = JsonConvert.SerializeObject(postForm);

            Dictionary<string, string> formVariables = JsonConvert.DeserializeObject<Dictionary<string, string>>(xhrForm);

            var formContent = new FormUrlEncodedContent(formVariables);

            return await PostAsync(requestUri, formContent);
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

            return await response.Content.ReadAsStringAsync();
        }


        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
