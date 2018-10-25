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



        public async Task<MaxLoginResponse> GetMaxTryNumber()
        {
            var response = await base.PostAsync(new Uri(base.BaseAddress, "GetMaxTryNumber"), new StringContent("", Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<MaxLoginResponse>(response);
        }

        public async Task<bool> LogIn(LoginCredentials loginCredentials)
        {
            var loginResponse = await CheckLoginEnable(loginCredentials);

            return loginResponse.IsSuccess == true && loginResponse.ErrorMessage.Equals("Sikeres bejelentkezés");
        }


        private async Task<LoginResponse> CheckLoginEnable(LoginCredentials loginCredentials)
        {
            string json = JsonConvert.SerializeObject(loginCredentials);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            string response = await base.PostAsync(new Uri(base.BaseAddress, "CheckLoginEnable"), stringContent);

            return InvalidAjaxResponseDeserializer.Fix<LoginResponse>(response);
        }
    }
}
