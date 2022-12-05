using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class ParentLoginPage : ContentPage
    {
        //The parent login page, checks that the email & password entered is in our database and send them to the MapPage if it is, also brings the user to the ParentSignUpPage
        //TODO: Needs a database to compare the entries to, currently just sends to MapPage when entries are filled
        public ParentLoginPage()
        {
            InitializeComponent();
        }

        //Checks that the entered email & password is in our database, and if it is it will navigate to MapPage
        //TODO: currently only checks that the email & password entries are filled and then sends to MapPage, need a database before being
        //able to implement a proper check on if the email & password are right
        void ParentLogIn_Clicked(System.Object sender, System.EventArgs e)
        {
            
            bool isEmailEmpty = string.IsNullOrEmpty(ParentEmail.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(ParentPassword.Text);
           
            //checks if email & password are filled
            if (isEmailEmpty)
            {
                //Displays pop-up saying that one of the entries are incorrect somehow
                DisplayAlert("Error", "Your email and/or password are inccorect, Please try again, or sign up if you don't have an account", "Ok");
            }
            else
            {
                //checks that the email & password match ones from our database, and then sends them to their student's MapPage
                //TODO: the logic for checking the entries with our database will be an if/else statement similar to the one for checking if the entries are filled,
                //if will see if the entries match & if not do nothing & else will navigate to the correct MapPage for the account
                Navigation.PushAsync(new MapPage());
            }
        }

        //Navigates to ParentSignUpPage
        void ParentSignUp_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ParentSignUpPage());
        }
    }
}

