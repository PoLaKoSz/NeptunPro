using NeptunPro.Models.XHR.Responses;
using Newtonsoft.Json;

namespace NeptunPro.Deserializers
{
    /// <summary>
    /// When an AJAX response come with a {"d" : "<something>"} this class will take care of it
    /// </summary>
    public static class InvalidAjaxResponseDeserializer
    {
        public static T Fix<T>(string ajaxResponse)
        {
            var jsonString = JsonConvert.DeserializeObject<InvalidJson>(ajaxResponse).RawJson;

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
