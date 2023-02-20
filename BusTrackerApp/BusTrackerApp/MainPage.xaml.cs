using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BusTrackerApp
{
    //the Welcome page, navigates to every user-side login page
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //Navigates to StudentLoginPage
        private void studentButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StudentLoginPage());
        }

        //Navigates to ParentLoginPage
        private void parentButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ParentLoginPage());
        }

        void driverButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DriverLoginPage());
        }
    }
}

