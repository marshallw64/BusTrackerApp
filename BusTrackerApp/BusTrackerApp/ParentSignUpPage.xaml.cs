using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class ParentSignUpPage : ContentPage
    {
        public ParentSignUpPage()
        {
            InitializeComponent();
        }

        void SendCode_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(ParentSignUpEmail.Text);

            if (isEmailEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new MapPage());
            }

            DisplayAlert("Success" ,"Your email has been sent", "Ok");


            
        }
    }
}

