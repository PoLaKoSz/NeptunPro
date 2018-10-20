using Newtonsoft.Json;

namespace NeptunPro.Models.XHR.Requests
{
    public class PostForm
    {
        [JsonProperty("__ASYNCPOST")]
        public bool AsyncPost { get; }

        [JsonProperty("__EVENTARGUMENT")]
        public string EventArgument { get; protected set; }

        [JsonProperty("__EVENTTARGET")]
        public string EventTarget { get; protected set; }

        [JsonProperty("__EVENTVALIDATION")]
        public string EventValidation { get; set; }

        [JsonProperty("__LASTFOCUS")]
        public string LastFocus { get; }

        [JsonProperty("__VIEWSTATE")]
        public string ViewState { get; set; }

        [JsonProperty("__VIEWSTATEGENERATOR")]
        public string ViewStateGenerator { get; set; }

        [JsonProperty("ActiveModalBehaviourID")]
        public int? ActiveModalBehaviourID { get; set; }

        [JsonProperty("filedownload$hfDocumentId")]
        public int? FileDownloadHfDocumentID { get; set; }

        [JsonProperty("hfCountDownTime")]
        public int HfCountDownTime { get; }

        [JsonProperty("hiddenEditLabel")]
        public string HiddenEditLabel { get; }

        [JsonProperty("NoMatchString")]
        public string NoMatchString { get; }

        [JsonProperty("progressalerttype")]
        public string ProgressAlertType { get; }

        [JsonProperty("ToolkitScriptManager1")]
        public string ToolkitScriptManager { get; set; }

        [JsonProperty("ToolkitScriptManager1_HiddenField")]
        public string ToolkitScriptManagerHiddenField { get; set; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkExam")]
        public string CalendarExamState { get; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkKonzultacio")]
        public string CalendarConsultationState { get; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkTask")]
        public string CalendarTaskState { get; }

        [JsonProperty("upBoxes$upCalendar$gdgCalendar$ctl35$calendar$upPanel$chkTime")]
        public string CalendarTimeState { get; }



        public PostForm()
        {
            AsyncPost = true;
            EventArgument = "";
            EventTarget = "";
            EventValidation = "";
            LastFocus = "";
            ViewState = "";
            ViewStateGenerator = "";
            HfCountDownTime = 300;
            HiddenEditLabel = "";
            NoMatchString = "A listában nincs ilyen elem!";
            ProgressAlertType = "progress";
            ToolkitScriptManager = "";
            ToolkitScriptManagerHiddenField = "";
            CalendarExamState = "on";
            CalendarConsultationState = "on";
            CalendarTaskState = "on";
            CalendarTimeState = "on";
        }



        /// <summary>
        /// Update the ASP.NET fields in this object
        /// </summary>
        /// <param name="postForm"></param>
        public void UpdateWith(PostForm postForm)
        {
            EventValidation = postForm.EventValidation;

            ViewState = postForm.ViewState;
            ViewStateGenerator = postForm.ViewStateGenerator;

            ToolkitScriptManagerHiddenField = postForm.ToolkitScriptManagerHiddenField;
        }
    }
}
