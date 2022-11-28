using System;
using System.Collections.Generic;
//using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BusTrackerApp
{

    //Will get parent's email and send the template email to it and then navigate to the CodePage, will generate 4 digit code here and put it in the template, as well as save it for the CodePage
    //Will also check that the email is valid before sending the template, all while informing the user if the email was sent or if there was an error
    public partial class ParentSignUpPage : ContentPage
    {
        public ParentSignUpPage()
        {
            InitializeComponent();
        }

        //Handles the email sending, code generation, email checking, and navigation to the CodePage
        //TODO: only checks if the entry is filled or not and displays the correct pop-ups for empty entries & sucessful emailing
        void SendCode_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(ParentSignUpEmail.Text);
            if (isEmailEmpty)
            {
                //checks that the email entry is a valid email (not empty, has an @, etc.)
                //TODO: only checks if the entry is filled, needs to also check for if the email is valid
                DisplayAlert("Error", "No email in textbox. Please enter Email","Ok");
                return;
            }
            else
            {
                //generates & saves 4 digit code  used for the CodePage & the email entered (keeps until verified & then puts in database for that user), will then navigate to the CodePage 
                //TODO: Needs to generate 4 digit code & send the template email to the right address with the code & email entered put in the template
                //will also need to save the code & email for the CodePage
                DisplayAlert("Success", "Your email has been sent", "Ok");
                Navigation.PushAsync(new CodePage());
            }

           


            
        }
    }
}

