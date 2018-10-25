using NeptunPro.DataAccessLayer.Web;
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



        public async Task<MaxLoginResponse> GetMaxTryNumber()
        {
            var response = await base.PostAsync(new Uri(base.BaseAddress, "GetMaxTryNumber"), new StringContent("", Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<MaxLoginResponse>(response);
        }

        public async Task LogIn(LoginCredentials loginCredentials)
        {
            await CheckLoginEnable(loginCredentials);
        }


        private async Task CheckLoginEnable(LoginCredentials loginCredentials)
        {
            string json = JsonConvert.SerializeObject(loginCredentials);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            string responseContent = await base.PostAsync(new Uri(base.BaseAddress, "CheckLoginEnable"), stringContent);
        }
    }
}
