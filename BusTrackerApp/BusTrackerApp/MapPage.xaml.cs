using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System.IO;
using Amazon.Runtime.CredentialManagement;
using Amazon;

namespace BusTrackerApp
{
    public partial class MapPage : ContentPage
    {

        IGeolocator locator = CrossGeolocator.Current;
        public MapPage()
        {

            InitializeComponent();

            //Draws lines where the route is with each coordinate being a start or end point of each line
            //It then adds the route on top the map
            //This is a tester route from New Tech to CHS
            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 12,
                Geopath =
                {
                    new Xamarin.Forms.Maps.Position(32.97183, -96.97242),
                    new Xamarin.Forms.Maps.Position(32.96951, -96.97237),
                    new Xamarin.Forms.Maps.Position(32.96944, -96.98761),
                    new Xamarin.Forms.Maps.Position(32.96930, -96.98900),
                    new Xamarin.Forms.Maps.Position(32.96932, -96.99365),
                    new Xamarin.Forms.Maps.Position(32.97305, -96.99368),
                    new Xamarin.Forms.Maps.Position(32.97296, -96.99720),
                    new Xamarin.Forms.Maps.Position(32.97321, -96.99763),
                    new Xamarin.Forms.Maps.Position(32.97359, -96.99721),
                    new Xamarin.Forms.Maps.Position(32.97471, -96.99721)

                }
            };

            AddBusPinAsync(49);
            busMap.MapElements.Add(polyline);
        }

        public MapPage(int busNum)
        {

            InitializeComponent();

            //Draws lines where the route is with each coordinate being a start or end point of each line
            //It then adds the route on top the map
            //This is a tester route from New Tech to CHS
            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 12,
                Geopath =
                {
                    new Xamarin.Forms.Maps.Position(32.97183, -96.97242),
                    new Xamarin.Forms.Maps.Position(32.96951, -96.97237),
                    new Xamarin.Forms.Maps.Position(32.96944, -96.98761),
                    new Xamarin.Forms.Maps.Position(32.96930, -96.98900),
                    new Xamarin.Forms.Maps.Position(32.96932, -96.99365),
                    new Xamarin.Forms.Maps.Position(32.97305, -96.99368),
                    new Xamarin.Forms.Maps.Position(32.97296, -96.99720),
                    new Xamarin.Forms.Maps.Position(32.97321, -96.99763),
                    new Xamarin.Forms.Maps.Position(32.97359, -96.99721),
                    new Xamarin.Forms.Maps.Position(32.97471, -96.99721)

                }
            };

            AddBusPinAsync(49);
            busMap.MapElements.Add(polyline);
        }

        //when the settings button is clicked, it sends you to the settings page
        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Centers the map on Coppell (around hertz)
            CenterMap(32.97091, -96.98583);
        }

        //This commented code (and line 15) is for getting the location of the user
        //Only useful for the driver to get their location and send it off to the database
        //TODO: Move all the commented code to a seperate class to get the driver's location and
        //      send it to the database where it will be sent to students and parents to be displayed on their maps
        private async void GetDriverLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

                busMap.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // promt the user to turn on the permission in settings
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }*/

        //Centers the map on a single coordinate
        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
            MapSpan span = new MapSpan(center, 0.05, 0.05);

            busMap.MoveToRegion(span);
        }

        //Adds a pin for a bus on to the map using the number given
        //TODO: Either move this method to another class with all the bus coordinates or add coordinates to the paramiters 
        private async Task AddBusPinAsync(int busNum)
        {

            float[] corrdinates = await retiveItemFromDB(49);

            //Creates new pin object with lable, type, and position properties
            Pin busPin = new Pin
            {
                Label = "Bus #" + busNum,
                Type = PinType.Generic,
                Position = new Xamarin.Forms.Maps.Position(corrdinates[0], corrdinates[1])
            };

            //Adds busPin to map
            busMap.Pins.Add(busPin);
        }

        async Task<float[]> retiveItemFromDB(int busNum)
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
                { "BusNum", new AttributeValue { N = busNum.ToString() } },
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
            Dictionary<string, AttributeValue> item = result.Item;

            float[] corrdinates = { float.Parse(item["driverLactionLat"].N), float.Parse(item["driverLocationLon"].N) };

            return corrdinates;
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
    }
}

