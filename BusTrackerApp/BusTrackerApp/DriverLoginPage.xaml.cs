using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class DriverLoginPage : ContentPage
    {
        public DriverLoginPage()
        {
            InitializeComponent();
        }

        void busDriverLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isBusNumEmpty = string.IsNullOrEmpty(busNumEntry.Text);

            if (isBusNumEmpty)
            {
                //displays error pop-up that says that that ID isn't valid in some way
                DisplayAlert("Error", "Invalid Bus Nummber", "Ok");
            }
            else
            {
                //navigates to MapPage if the ID is valid, navigates to user's saved routes MapPage if applicable
                //TODO: needs to access user's saved routes to take user to the correctly modified MapPage
                Navigation.PushAsync(new MapPage(int.Parse(busNumEntry.Text)));
            }
        }
    }
}

