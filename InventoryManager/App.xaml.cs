using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventoryManager
{
    public partial class App : Application
    {
        public static string _token { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new LoginPage();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
