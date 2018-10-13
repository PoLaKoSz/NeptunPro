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
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
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
