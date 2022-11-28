using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class ParentLoginPage : ContentPage
    {
        public ParentLoginPage()
        {
            InitializeComponent();
        }

        void ParentLogIn_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(ParentEmail.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(ParentPassword.Text);

            if (isEmailEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new MapPage());
            }
        }

        void ParentSignUp_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ParentSignUpPage());
        }
    }
}

