using NeptunPro.Models.XHR.Responses;
using Newtonsoft.Json;
using System;

namespace NeptunPro.Parsers
{
    /// <summary>
    /// When an AJAX response come with a {"d" : "<something>"} this class will take care of it
    /// </summary>
    internal class InvalidAjaxResponseParser : Logger
    {
        /// <summary>
        /// Deserialize {"d" : "<something>"} string into C# object
        /// <summary>
        /// <typeparam name="T">Object to deserialized</typeparam>
        /// <param name="ajaxResponse">The whole string from the server</param>
        /// <exception cref="FormatException">Given string couldn't be parsed to the specified class</exception>
        internal static T Fix<T>(string ajaxResponse)
        {
            try
            {
                var jsonString = JsonConvert.DeserializeObject<InvalidJson>(ajaxResponse).RawJson;

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't parse invalid {ajaxResponse} XHR response to valid one!", ajaxResponse);
                throw new FormatException("Couldn't parse invalid XHR response to valid one.", ex);
            }
        }
    }
}
