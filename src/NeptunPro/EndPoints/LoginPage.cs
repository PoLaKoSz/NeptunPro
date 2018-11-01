using NeptunPro.DataAccessLayer.Web;
using NeptunPro.Deserializers;
using NeptunPro.Models;
using NeptunPro.Models.XHR.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeptunPro.EndPoints
{
    public class LoginPage : EndPoint
    {
        public LoginPage()
            : base(new Uri("https://neptun.uni-obuda.hu/hallgato/Login.aspx/")) { }



        public async Task<NeptunBuildDetails> GetBuildDetails()
        {
            var response = await base.GetAsync(base.BaseAddress);

            System.IO.File.WriteAllText("index.html", response, Encoding.UTF8);

            return LoginPageDeserializer.BuildDetails(response);
        }

        public async Task<MaxLoginResponse> GetMaxTryNumber()
        {
            var response = await base.PostAsync(new Uri(base.BaseAddress, "GetMaxTryNumber"), new StringContent("", Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<MaxLoginResponse>(response);
        }

        /// <summary>
        /// Authenticate to the Neptun system to have access every endpoint.
        /// </summary>
        /// <param name="loginCredentials">Username and Password</param>
        /// <returns>Response from the server</returns>
        public async Task<LoginResponse> Authenticate(LoginCredentials loginCredentials)
        {
            string json = JsonConvert.SerializeObject(loginCredentials);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            string response = await base.PostAsync(new Uri(base.BaseAddress, "CheckLoginEnable"), stringContent);

            return InvalidAjaxResponseDeserializer.Fix<LoginResponse>(response);
        }
    }
}
