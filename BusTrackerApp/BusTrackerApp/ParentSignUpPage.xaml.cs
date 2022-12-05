using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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
            Random rand = new Random();
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
                var code = rand.Next(0000, 9999);

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("dumminer19@gmail.com");
                message.To.Add(new MailAddress("mawhitewater@icloud.com"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<p>Please Help I'm trapped in an Email</p>";
                smtp.Port = 465;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dumminer19@gmail.com", "worvyd-mYwwiv-pumga2");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                DisplayAlert("Success", "Your email has been sent " + code, "Ok");

                /*MailAddress from = new MailAddress("mawhitewater@icloud.com");
                MailAddress to = new MailAddress("mwhitewater@newtech.coppellisd.com");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Using the SmtpClient class.";
                message.Body = @"Using this feature, you can send an email message from an application very easily.";
                SmtpClient client = new SmtpClient("207.235.150.111");

                client.Send(message);*/

                /*var smtpClient = new SmtpClient("mawhitewater@icloud.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mawhitewater@icloud.com", ""),
                    EnableSsl = true,
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("mwhitewater@newtech.coppellisd.com"),
                    Subject = "Your Father",
                    Body = "<p>Please Help I'm trapped in an Email</p>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add("recipient");

                smtpClient.Send(mailMessage);*/
                Navigation.PushAsync(new CodePage());
            }





        }
    }
}

