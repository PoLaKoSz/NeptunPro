using System;
using System.Net.Http;

namespace NeptunPro.DataAccessLayer.Web
{
    public abstract class EndPoint : IDisposable
    {
        protected static readonly HttpClient _client;

        protected Uri BaseAddress { get; private set; }



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



        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
