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
using System.Threading;
using BusTrackerApp.DataTables;

namespace BusTrackerApp
{
    public partial class MapPage : ContentPage
    {
        //Establishes a connection the AWSConnection class for database methods
        AWSConnection awsDB = new AWSConnection();

        //Creates the pin for the bus to use
        Pin busPin = null;

        //declares a boolean the says whether or not 
        bool isChecking = false;

        //Declares a integer for the number that bus uses that will be used for all the logic here
        int busNum = 0;

        IGeolocator locator = CrossGeolocator.Current;

        //This constructor
        public MapPage()
        {
            InitializeComponent();

            CrossGeolocator.Current.DesiredAccuracy = 20;

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
            isChecking = true;
            Task.Factory.StartNew(() => { checkDBForChanges(49); });

            busMap.MapElements.Add(polyline);
        }

        //This constuctor is for the driver so it can track their location and draw their map
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

            this.busNum = busNum;

            GetDriverLocation();
            busMap.MapElements.Add(polyline);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            isChecking = false;

            locator.StopListeningAsync();
        }

        private async void checkDBForChanges(int busNum)
        {
            var numOfTries = 0;
            while (numOfTries < 10)
            {
                try
                {
                    while (isChecking)
                    {
                        float[] newCorrdinates = await awsDB.retiveItemFromDB(busNum);

                        if (newCorrdinates[0] != busPin.Position.Latitude || newCorrdinates[1] != busPin.Position.Longitude)
                        {
                            busPin = new Pin { Label = "Bus #" + busNum, Type = PinType.Generic, Position = new Xamarin.Forms.Maps.Position(newCorrdinates[0], newCorrdinates[1]) };
                        }
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception)
                {
                    //Solution 1
                    numOfTries++;
                    Console.WriteLine("checkDBForChanges Failed");
                    //Solution 2
                    //checkDBForChanges(busNum);
                }
            }
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
                var location = CrossGeolocator.Current;

                locator.PositionChanged += async (sender, e) => {
                    try
                    {
                        await awsDB.saveItemToDB(busNum, e.Position.Latitude, e.Position.Longitude);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Save location failed");
                    }
                }; ;
                await locator.StartListeningAsync(new TimeSpan(0, 0, 30), 1);

                busMap.IsShowingUser = true;
            }
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
        }

        //Centers the map on a single coordinate
        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
            MapSpan span = new MapSpan(center, 0.05, 0.05);

            busMap.MoveToRegion(span);
        }

        //Adds a pin for a bus on to the map using the number given
        //TODO: Either move this method to another class with all the bus coordinates or add coordinates to the paramiters 
        private async void AddBusPinAsync(int busNum)
        {

            float[] corrdinates = await awsDB.retiveItemFromDB(busNum);

            Console.WriteLine(corrdinates[0] + " " + corrdinates[1]);

            //Creates new pin object with lable, type, and position properties
            Pin busPin = new Pin
            {
                Label = "Bus #" + busNum,
                Type = PinType.Generic,
                Position = new Xamarin.Forms.Maps.Position(corrdinates[0], corrdinates[1])
            };

            this.busPin = busPin;

            busMap.Pins.Add(busPin);
        }
    }
}