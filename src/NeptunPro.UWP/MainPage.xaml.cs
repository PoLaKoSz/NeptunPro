using System;

namespace NeptunPro.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new NeptunPro.App());
        }
    }
}
