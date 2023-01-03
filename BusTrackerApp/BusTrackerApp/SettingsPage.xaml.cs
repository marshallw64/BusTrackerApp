using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        void signOut_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}

