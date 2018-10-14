using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Requests
{
    public class Message
    {
        [JsonProperty("__ASYNCPOST")]
        public bool AsyncPost { get; set; }

        [JsonProperty("__EVENTARGUMENT")]
        public string EventArgument { get; set; }

        [JsonProperty("__EVENTTARGET")]
        public string EventTarget { get; set; }

        [JsonProperty("__EVENTVALIDATION")]
        public string EventValidation { get; set; }

        [JsonProperty("__LASTFOCUS")]
        public string LastFocus { get; set; }

        [JsonProperty("__VIEWSTATE")]
        public string ViewState { get; set; }

        [JsonProperty("__VIEWSTATEGENERATOR")]
        public string ViewStateGenerator { get; set; }

        [JsonProperty("ActiveModalBehaviourID")]
        public int? ActiveModalBehaviourID { get; set; }

        [JsonProperty("filedownload$hfDocumentId")]
        public int? FileDownloadHfDocumentID { get; set; }

        [JsonProperty("hfCountDownTime")]
        public int HfCountDownTime { get; set; }

        [JsonProperty("hiddenEditLabel")]
        public string HiddenEditLabel { get; set; }

        [JsonProperty("NoMatchString")]
        public string NoMatchString { get; set; }

        [JsonProperty("progressalerttype")]
        public string ProgressAlertType { get; set; }

        [JsonProperty("ToolkitScriptManager1")]
        public string ToolkitScriptManager { get; set; }

        [JsonProperty("ToolkitScriptManager1_HiddenField")]
        public string ToolkitScriptManagerHiddenField { get; set; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkExam")]
        public string CalendarExamState { get; set; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkKonzultacio")]
        public string CalendarConsultationState { get; set; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkTask")]
        public string CalendarTaskState { get; set; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkTime")]
        public string CalendarTimeState { get; set; }

        [JsonProperty("upFilter$rblMessageTypes")]
        public string MessageFilter { get; set; }

        [JsonProperty("upFunction$c_messages$upMain$hfDocumentId")]
        public int? HfDocumentID { get; set; }

        [JsonProperty("upFunction$c_messages$upMain$upFilter$searchpanel$searchpanel_state")]
        public string SearchPanelState { get; }

        [JsonProperty("upFunction$c_messages$upModal$upmodalextenderReadMessage$_data")]
        public string MessageModalState { get; }



        public Message()
        {
            AsyncPost = true;
            HfCountDownTime = 300;
            NoMatchString = "A listában nincs ilyen elem!";
            ProgressAlertType = "progress";
            ToolkitScriptManager = "ToolkitScriptManager1|upFunction$c_messages$upMain$upGrid$gridMessages";
            CalendarExamState = "on";
            CalendarConsultationState = "on";
            CalendarTaskState = "on";
            CalendarTimeState = "on";
            SearchPanelState = "expanded";
            MessageModalState = "Visible:false";
        }
    }
}
