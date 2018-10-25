using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Responses
{
    public class MaxLoginResponse
    {
        [JsonProperty("d")]
        public int TryCount { get; }



        public MaxLoginResponse(int tryCount)
        {
            TryCount = tryCount;
        }
    }
}
