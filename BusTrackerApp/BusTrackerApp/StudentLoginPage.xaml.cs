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

        async void testRouteAWS_Clicked(System.Object sender, System.EventArgs e)
        {
            if (await Task.Run(saveItemToDB))
            {
                Console.Write("It worked yay!!!!!!!!!!!!!!!!!!");
            } else
            {
                Console.Write("It didn't. Go back to fart");
            }
            
        }

        void WriteProfile(string profileName, string keyId, string secret)
        {
            Console.WriteLine($"Create the [{profileName}] profile...");
            var options = new CredentialProfileOptions
            {
                AccessKey = keyId,
                SecretKey = secret
            };
            var profile = new CredentialProfile(profileName, options);
            var sharedFile = new SharedCredentialsFile();
            sharedFile.RegisterProfile(profile);
            Console.Write("New Profile Saved");
        }

        async Task<bool> saveItemToDB()
        {
            WriteProfile("default", "AKIA6LJNZIF7QCKALEUV", "DwEL+pPJTqpe6i+JtJmEoD3vdo28U+YDG/1FnXGD");

            /*
             * Got Cliet Config from 
             * https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/CodeSamples.DotNet.html
             */
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the US East 2 region.
            clientConfig.RegionEndpoint = RegionEndpoint.USEast2;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);

            DynamoDBContext context = new DynamoDBContext(client);

            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();

            //MemoryStream beeboo = new MemoryStream(132);

            byte[] byteArray = { 0 };

            MemoryStream beeboo = new MemoryStream(byteArray);

            attributes["BusNum"] = new AttributeValue { N = "56" };
            attributes["IsAM"] = new AttributeValue { B = beeboo };
            attributes["driverLactionLat"] = new AttributeValue { N = "32.96945" };
            attributes["driverLocationLon"] = new AttributeValue { N = "-96.99385" };


            Console.WriteLine("I'm saving an item");

            // Create PutItem request
            PutItemRequest request = new PutItemRequest
            {
                TableName = "BusRoutes",
                Item = attributes
            };

            Console.WriteLine("I'm still saving an item");
            //Save method Document Model
            //Issue PutItem request
            var response = await client.PutItemAsync(request);
            Console.WriteLine("entry saved");
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }



    }
}

