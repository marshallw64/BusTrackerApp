using System;
using System.Collections.Generic;
//using System.Text.RegularExpressions;
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
            //bool IsEmailActual = IsVa
            if (isEmailEmpty)
            {
                DisplayAlert("Error", "No email in textbox. Please enter Email","Ok");
                return;
            }
            else
            {
                
                DisplayAlert("Success", "Your email has been sent", "Ok");
                Navigation.PushAsync(new CodePage());
            }

           


            
        }
    }
}

