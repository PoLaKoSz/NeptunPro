using Newtonsoft.Json;

namespace NeptunPro.Models
{
    public class LoginCredentials
    {
        [JsonProperty("user")]
        public string UserName { get; }

        [JsonProperty("pwd")]
        public string Password { get; }

        [JsonProperty("UserLogin")]
        public string UserLogin { get; }

        [JsonProperty("GUID")]
        public int? GUID { get; }

        [JsonProperty("captcha")]
        public string Captcha { get; }



        public LoginCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
