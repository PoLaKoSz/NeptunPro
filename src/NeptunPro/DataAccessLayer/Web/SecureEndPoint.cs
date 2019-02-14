using NeptunPro.Parsers;
using NeptunPro.Models.XHR.Requests;
using System;
using System.Threading.Tasks;

namespace NeptunPro.DataAccessLayer.Web
{
    /// <summary>
    /// This class need to be used in most cases (when calling an endpoint and it require
    /// some kind of authentication [__VIEWSTATE, __EVENTVALIDATION, __EVENTARGUMENT, etc])
    /// insead of <see cref="EndPoint"/> because this takes care of it
    /// </summary>
    public abstract class SecureEndPoint : EndPoint
    {
        private static readonly PostForm _geneticPostForm;



        static SecureEndPoint()
        {
            _geneticPostForm = new PostForm();
        }

        public SecureEndPoint(Uri baseAddress)
            : base(baseAddress) { }



        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <exception cref="HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        protected new async Task<string> GetAsync(Uri requestUri)
        {
            string response = await base.GetAsync(requestUri);

            HiddenFormParser.FromWholePage(_geneticPostForm, response);

            return response;
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="postForm">Form sent to the server.</param>
        /// <exception cref="HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        protected new async Task<string> PostAsync(Uri requestUri, PostForm postForm)
        {
            postForm.UpdateWith(_geneticPostForm);

            string response = await base.PostAsync(requestUri, postForm);

            HiddenFormParser.FromApiResponse(_geneticPostForm, response);

            return response;
        }
    }
}
