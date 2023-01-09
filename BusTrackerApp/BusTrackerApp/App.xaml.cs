using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusTrackerApp
{
    public partial class App : Application
    {

        public static string DataBaseLocation = string.Empty;
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new MainPage());
        }

        public App(String dataBaseLoction)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DataBaseLocation = dataBaseLoction;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

