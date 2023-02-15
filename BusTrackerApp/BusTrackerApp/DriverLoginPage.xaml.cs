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

        void DriverLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isPasswordEmpty = string.IsNullOrEmpty(DriverPassword.Text);
            //bool isRoutePickerEmpty = string.IsNullOrEmpty(BusNumberPicker.Text);

            //checks if email & password are filled
            if (isPasswordEmpty)
            {
                //Displays pop-up saying that one of the entries are incorrect somehow
                DisplayAlert("Error", "Your password is inccorect, Please try again", "Ok");
            }
            else
            {
                //checks that the email & password match ones from our database, and then sends them to their student's MapPage
                //TODO: the logic for checking the entries with our database will be an if/else statement similar to the one for checking if the entries are filled,
                //if will see if the entries match & if not do nothing & else will navigate to the correct MapPage for the account
                Navigation.PushAsync(new MapPage());
            }
        }
    }
}

