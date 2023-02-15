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
            //Creates the AWS profile to access our AWS server
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

            //Creates a dictionary with attributes that will hold the data that will be inserted into the AWS table
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();

            //Creates a byte array with only 1 element that is either 0 (route is not in the morning) or 1 (route is in the morning)
            byte[] byteArray = { 1 };
            MemoryStream isAM = new MemoryStream(byteArray);

            //Creates 
            attributes["BusNum"] = new AttributeValue { N = "49" };
            attributes["IsAM"] = new AttributeValue { B = isAM };
            attributes["driverLactionLat"] = new AttributeValue { N = "32.96999" };
            attributes["driverLocationLon"] = new AttributeValue { N = "-96.99399" };


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

        async void testRetrivingRouteAWS_Clicked(System.Object sender, System.EventArgs e)
        {
            if (await Task.Run(retiveItemFromDB))
            {
                Console.Write("It worked yay!!!!!!!!!!!!!!!!!!");
            }
            else
            {
                Console.Write("It didn't. Go back to fart");
            }
        }

        async Task<bool> retiveItemFromDB()
        {
            //Creates the AWS profile to access our AWS server
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

            byte[] byteArray = { 1 };
            MemoryStream isAM = new MemoryStream(byteArray);

            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "BusNum", new AttributeValue { N = "49" } },
                { "IsAM", new AttributeValue { B = isAM } }
            };

            // Create GetItem request
            GetItemRequest request = new GetItemRequest
            {
                TableName = "BusRoutes",
                Key = key,
            };

            // Issue request
            var result = await client.GetItemAsync(request);

            // View response
            Console.WriteLine("Bus Route:");
            Dictionary<string, AttributeValue> item = result.Item;
            foreach (var keyValuePair in item)
            {
                Console.WriteLine("Bus Num = {0}", item["BusNum"].N);
                Console.WriteLine("IsAM = {0}", ((byte[])item["IsAM"].B.ToArray()).GetValue(0));
                Console.WriteLine("driverLactionLat = {0}", item["driverLactionLat"].N);
                Console.WriteLine("driverLocationLon = {0}", item["driverLocationLon"].N);
            }

            return result.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}

