using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Responses
{
    public class LoginResponse
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public string ErrorCode { get; }
        public string WarningMessage { get; }



        public LoginResponse(
            [JsonProperty("success")] bool isSuccess,
            [JsonProperty("errormessage")] string errorMessage,
            [JsonProperty("errorcode")] string errorCode,
            [JsonProperty("warningmessage")] string warningMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            WarningMessage = warningMessage;
        }
    }
}
