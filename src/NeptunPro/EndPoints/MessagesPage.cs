using NeptunPro.DataAccessLayer.Web;
using NeptunPro.Deserializers;
using NeptunPro.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeptunPro.EndPoints
{
    public class MessagesPage : EndPoint
    {
        public MessagesPage()
            : base(new Uri("https://neptun.uni-obuda.hu/hallgato/main.aspx")) { }



        /// <summary>
        /// Load the <see cref="Message"/>s from the InBox first page
        /// </summary>
        /// <returns>Collection of <see cref="Message"/> object which only has an ID, Sender, Subject and Title property</returns>
        public async Task<List<Message>> Load()
        {
            var response = await _client.GetAsync(base.BaseAddress);

            string sourceCode = await response.Content.ReadAsStringAsync();

            return MessagesPageDeserializer.InBox(sourceCode);
        }

        /// <summary>
        /// Get more details for the parameter <see cref="Message"/>
        /// </summary>
        /// <returns><see cref="Message"/> with the Text property</returns>
        public async Task GetMessage(Message message)
        {
            //var stringContent = new StringContent("ToolkitScriptManager1=ToolkitScriptManager1%7CupFunction%24c_messages%24upMain%24upGrid%24gridMessages&ToolkitScriptManager1_HiddenField=&ActiveModalBehaviourID=&progressalerttype=progress&NoMatchString=A%20list%C3%A1ban%20nincs%20ilyen%20elem!&hiddenEditLabel=&hfCountDownTime=300&upBoxes%24upCalendar%24gdgCalendar%24ctl35%24calendar%24upPanel%24chkTime=on&upBoxes%24upCalendar%24gdgCalendar%24ctl35%24calendar%24upPanel%24chkExam=on&upBoxes%24upCalendar%24gdgCalendar%24ctl35%24calendar%24upPanel%24chkTask=on&upBoxes%24upCalendar%24gdgCalendar%24ctl35%24calendar%24upPanel%24chkKonzultacio=on&upFilter%24rblMessageTypes=%C3%96sszes%20%C3%BCzenet&upFunction%24c_messages%24upMain%24hfDocumentId=&upFunction%24c_messages%24upMain%24upFilter%24searchpanel%24searchpanel_state=expanded&upFunction%24c_messages%24upModal%24upmodalextenderReadMessage%24_data=Visible%3Afalse&filedownload%24hfDocumentId=&__EVENTTARGET=upFunction%24c_messages%24upMain%24upGrid%24gridMessages&__EVENTARGUMENT=commandname%3DSubject%3Bcommandsource%3Dselect%3Bid%3D435373899%3Blevel%3D1&__LASTFOCUS=&__VIEWSTATE=%2FwEPaA8FDzhkNjE1YjQ5NGU0MDc2MRgCBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WFwUJYnRuTGFuZ18wBQlidG5MYW5nXzEFCWJ0bkxhbmdfMgULYnRuc2tpblBpbmsFC2J0bnNraW5CbHVlBQxidG5za2luR3JlZW4FDWJ0bnNraW5PcmFuZ2UFDmJ0bnNraW5UZWFjaGVyBQ1idG5za2luUHVycGxlBR5pbWdTa2luQ2hvb3NlclBhcnRpYWxseVNpZ2h0ZWQFHXVwQm94ZXMkdXBCb3hlc0J1dHRvbnMkYnRuUnNzBSF1cEJveGVzJHVwQm94ZXNCdXR0b25zJGJ0bk1lc3NhZ2UFInVwQm94ZXMkdXBCb3hlc0J1dHRvbnMkYnRuRmF2b3JpdGUFInVwQm94ZXMkdXBCb3hlc0J1dHRvbnMkYnRuQ2FsZW5kYXIFH3VwQm94ZXMkdXBCb3hlc0J1dHRvbnMkYnRuRm9ydW0FJnVwQm94ZXMkdXBSU1MkZ2RnUlNTJGdkZ1JTU19SZWZyZXNoQnRuBSR1cEJveGVzJHVwUlNTJGdkZ1JTUyRnZGdSU1NfQ2xvc2VCdG4FMnVwQm94ZXMkdXBNZXNzYWdlJGdkZ01lc3NhZ2UkZ2RnTWVzc2FnZV9SZWZyZXNoQnRuBTB1cEJveGVzJHVwTWVzc2FnZSRnZGdNZXNzYWdlJGdkZ01lc3NhZ2VfQ2xvc2VCdG4FNXVwQm94ZXMkdXBDYWxlbmRhciRnZGdDYWxlbmRhciRnZGdDYWxlbmRhcl9SZWZyZXNoQnRuBTN1cEJveGVzJHVwQ2FsZW5kYXIkZ2RnQ2FsZW5kYXIkZ2RnQ2FsZW5kYXJfQ2xvc2VCdG4FOnVwQm94ZXMkdXBGb3J1bSRnZGdGb3J1bSR1cFBhcmVudCRnYWRnZXQkZ2FkZ2V0X1JlZnJlc2hCdG4FOHVwQm94ZXMkdXBGb3J1bSRnZGdGb3J1bSR1cFBhcmVudCRnYWRnZXQkZ2FkZ2V0X0Nsb3NlQnRuBVd1cEJveGVzJHVwRm9ydW0kZ2RnRm9ydW0kdXBQYXJlbnQkZ2FkZ2V0JGN0bDM1JFZTX0Zhdm91cml0ZVRvcGljc19nYWRTbWFsbDEkbHZGYXZUb3BpY3MPPCsADgMIZgxmDQL%2F%2F%2F%2F%2FD2RmH92ud%2Fl2IK9uvmTRTDKuMZClCDUmDACBHXcNRijf8w%3D%3D&__VIEWSTATEGENERATOR=BF221877&__EVENTVALIDATION=%2FwEdAGntpDQLPb%2FFUkhd1KSv03zGPDCaPJoEjfecDi9%2BjCMpOmyPtOBtpkTuhpTwlqHooX98U8Mn26tkxt8TvQ7lrkcsgu8tbxyyBWLhoQ3Zy32z6Kk6lolxovqfp%2B1WKWDSGnd8ACKbcfdFEvOBts6gF8LUUyTA5RwlrIPW9NlaRvVNsD4l7Uj1YwyuRH9ioQYXeBrm5Y4J5Z1Kjf35uzPw4rvSEz2Oo1Q7jrF4bMPqIbCenfHojU5jsSEeFfyoME0Avg6p2C5km39g1z7LbCekzXLB3ML7rxDBTrZkN9P%2Fw%2FFH3Lq7qndIOYBhyGWkNPKBdXFcOKAwl07zqEfM%2FtrhhxjCTtN00PbxWRAw7zW1D8pJ78LJzNjObvnaf%2BEwPfNIEogeXwh%2FiW4CdqiC%2FY7iLDHXTABYlEIVdlXT7KPgDG0YXL43IX3O6lh6Q8sDU9f%2BElmaGFVLkaoMtACXiv4UrgTgHpoDPJD4DpoUOF6GWFCsj4vO7sUHbhldz%2FiLtGCauSMJ%2B1nozZ9utZ6CwGLTiSm0KP4WIdITKIqdgvLEiu54xaw%2BeLMrKOXVAK3OtxWEMhZdKWKiO9daA3YfuXKVyCE88lMnaFI1mu13n6zrRCoCTxS9s%2BlAAHhPV7JVic78gGdBT0%2BB3Goccy3Z0lKGYBwYR%2BIo6nsrkDewtwTHT2x2qQ9EbqUVtzaynvEti73l3fdO%2FoIRBo%2FZyCjC81tCPSV1aEbQeT0vE6iuuR19hSkTRION1WFO0%2FBvB%2BBBt4HBT4o3J%2F%2FjqNFK4MLVzxTSmBtszPnXnXsWluZ8Kg3i38II3OCr5pItdErz%2FXNy3YVrBDukcG4Krv0blSzbgg6ULB%2BOz7f0jtF0Mx%2FvpdeqX4A4Eey5CtvgfKqKtBiY2kOE3Xnua9uLM5CwvEr%2BQZ8hKV5wHqsgOAsIKG8bZ%2B4Hn1ifKyEoBnwiK4geVPE9eUDzoPQVcFNtoJeI3nLRyimxLbEY4AX4v%2BwBZcB%2F0b2yL1lqNZSoDFEreIdLOLzPXddmuW%2FcEXClsFl07Oa5kc2neJxWGk1naWDJziLEyRG%2F5mLEVaI8tsxhN53hW6TNUmpZUo04Rok5pxUaehhVHIUwaC53cV16%2FHLq0HT1hzL%2BFJU3l6GlnD5XGCnYXsmFk%2BsZYSPya2YPi94y1ITVIecPRcYLu0uOE8NkszVUpwxDQN1Wdnz3REibZxReMSRSyau9QpveneDRvWdp22ud4TXw8UcEdivzh9Gsg66%2FIUy2mBkTtusmHkIbbSik3C3tuItyaQxhUFiMS42yqFZOV6HZAoPSdjvDZzx4DbcY%2Br9DKgpKeDgnRdnXQUnp34N7ehpH8opZOoVYV7kQ5I7TILNP%2FNKDyD1kRAP%2Fponrhbn%2F9s%2B5RMFXSa5mOLWo5e1obIfUIMaDxkHVFo0uWrMB2k60ceTJsqqB1Enl%2FbHZ0R9rLFluoOwqZaRBZgXDexVk4JlEq9GnjDC9rfWcvsWvZGNcAXotwrA91gPXtYRHd%2B184m3oJ4z7wuX6rF4B1MQo6ZFOWOEIQa8EYUUwlhw4iWGa%2FhgD9F5TpmzywEIwAOjwzGwJ5%2FSSpRVkkHLVoPpA%2Fiv0w4DHlvahbwRe0fqxCK6m4CHTCXsYNf8nozZxHyYtCr0nwiTT%2Fsh3IAoLEt8dgBh2C0ahNrgl2WE%2Bu4w6D%2BgPOOMECr8IYMqlNCpHJJYn98y99TfgTNVE30Hc3gWnzlfm1oI4FdrONd%2B7rKb6jdGuFGP6TJmp7Gn2H5H0tGoSp1VA9XeRieheoT4h117eT3fRUAhYm6onjXeJgLwzXaK%2B7niBUNHSOc7nDEV48EDDZ6FELY%2B7rYU8KqEi16n4g5CNlqKihK14xCN%2FqyeEI6dg13UMIvWPd%2FY6JuPWhCxaMzjapomaMNdMDGvKqHTCGq9w4IP%2B%2FKAEQ7RAoIduwNQ%2FFYJvS6PmI7dgXEOv%2BDMo%2FPpgny0Zk6cUpHVmgJVjD2ehZxS4iZbQsyt3PYit0LvhNc298LG7E1FW0wbitPGbt%2FGiVsnKeelUwjcrC6sjCuFqOPcFCgGtUml2dI520yFsETkDP3b2XrkzZUQj07F8W0f60dA6bCVmtDE8YutXkmtyYR2teAX80Xh5OvHJ9Pu2Wtrb82V%2BGLpDtrpwLb9VKIH84iJVyGYcOA8v56vlcQXxpAkxNg%2FMfJd3ec4mXulYlk0DranB4lmtK4VyDtM7dQaaziikf33erUQ7dynIG1N1%2FTNx3Kf7W%2BhXwpXt%2FaWkQ%2FsTx%2BH6lMo55a4olshZ%2FpFSNQ%3D%3D&__ASYNCPOST=true&", Encoding.UTF8, "application/json");

            //var response = await _client.PostAsync(base.BaseAddress, stringContent);

            //var responseContent = response.Content.ReadAsStringAsync();
        }
    }
}
