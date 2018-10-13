using NeptunPro.EndPoints;
using NeptunPro.Models;
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
                throw new Exception("Fill up Your Username, please!");

            await loginPage.GetMaxTryNumber();
            await loginPage.LogIn(new LoginCredentials(userName, password));

            Current.Shutdown();
        }
    }
}
