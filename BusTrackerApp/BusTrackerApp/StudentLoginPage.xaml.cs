using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using static BusTrackerApp.DataTables.AWSConnection;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Endpoints;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.CredentialManagement;
using System.IO.Compression;
using System.IO;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace BusTrackerApp
{
    //Where a student logs in, will compare the entered ID to our database to verify it, will also save that user/ID's route once already entered during their 1st time
    //TODO: only checks if the ID entry is empty, but will send them to the MapPage if it is, needs database & database logic
    public partial class StudentLoginPage : ContentPage
    {
        public StudentLoginPage()
        {
            InitializeComponent();
            Console.WriteLine("Where am I?");
        }

        //checks that the ID is valid and sends student to the MapPage, displaying saved routes if the ID is assosiated with any
        //TODO: only checks if entry is filled, needs database & database logic
        private void studentLogin_Clicked(object sender, EventArgs e)
        {
            bool isIDEmpty = string.IsNullOrEmpty(IdEntry.Text);

            if (isIDEmpty) 
            {
                //displays error pop-up that says that that ID isn't valid in some way
                DisplayAlert("Error", "Invalid ID, your ID is your 5-digit lunch code", "Ok");
            }
            else
            {
                //navigates to MapPage if the ID is valid, navigates to user's saved routes MapPage if applicable
                //TODO: needs to access user's saved routes to take user to the correctly modified MapPage
                Navigation.PushAsync(new MapPage());
            }
        }
    }
}

