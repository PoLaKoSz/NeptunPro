using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Requests
{
    public class PostMessageForm : PostForm
    {
        [JsonProperty("upFilter$rblMessageTypes")]
        public string MessageFilter { get; set; }

        [JsonProperty("upFunction$c_messages$upMain$hfDocumentId")]
        public int? HfDocumentID { get; set; }

        [JsonProperty("upFunction$c_messages$upMain$upFilter$searchpanel$searchpanel_state")]
        public string SearchPanelState { get; }

        [JsonProperty("upFunction$c_messages$upModal$upmodalextenderReadMessage$_data")]
        public string MessageModalState { get; }



        public PostMessageForm()
        {
            EventTarget = "upFunction$c_messages$upMain$upGrid$gridMessages";
            ToolkitScriptManager = "ToolkitScriptManager1|upFunction$c_messages$upMain$upGrid$gridMessages";

            MessageFilter = "Összes üzenet";
            SearchPanelState = "expanded";
            MessageModalState = "Visible:false";
        }



        public void SetID(Message message)
        {
            EventArgument = "commandname=Subject;commandsource=select;id=" + message.ID + ";level=1";
        }
    }
}
