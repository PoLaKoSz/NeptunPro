using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Responses
{
    public class InvalidJson
    {
        public string RawJson { get; }



        public InvalidJson([JsonProperty("d")]string raw)
        {
            RawJson = raw;
        }
    }
}
