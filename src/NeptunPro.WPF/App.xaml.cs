using NeptunPro.EndPoints;
using NeptunPro.Models;
using NeptunPro.Models.XHR.Responses;
using System;
using System.Windows;

namespace NeptunPro.WPF
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginPage = new LoginPage();
            string userName = "BH6YG8";
            string password = "";

            if (password.Equals(""))
                throw new Exception("Fill up Your Password, please!");

            var maxTry = await loginPage.GetMaxTryNumber();
            var loginResponse = await loginPage.Authenticate(new LoginCredentials(userName, password));

            if (!loginResponse.IsSuccess)
            {

            }


            var messagesPage = new MessagesPage();
            var messsages = await messagesPage.Load();

            var specificMessage = await messagesPage.GetMessage(messsages[0]);

            Current.Shutdown();
        }
    }
}
