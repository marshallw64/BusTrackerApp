using System;
using Amazon.DynamoDBv2;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.CredentialManagement;
using Xamarin.Forms.Maps;
using Amazon;

namespace BusTrackerApp.DataTables
{
	public class AWSConnection
	{
        public async Task<bool> saveItemToDB(int busNum, double driverLoctionLat, double driverLocationLong)
        {
            //Creates the AWS profile to access our AWS server
            //WriteProfile("default", "AKIA6LJNZIF7QCKALEUV", "DwEL+pPJTqpe6i+JtJmEoD3vdo28U+YDG/1FnXGD");

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
            attributes["BusNum"] = new AttributeValue { N = busNum.ToString() };
            attributes["IsAM"] = new AttributeValue { B = isAM };
            attributes["driverLactionLat"] = new AttributeValue { N = driverLoctionLat.ToString() };
            attributes["driverLocationLon"] = new AttributeValue { N = driverLocationLong.ToString() };


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

        public async Task<float[]> retiveItemFromDB(int busNum)
        {
            Console.WriteLine("Scanning DB");
            //Creates the AWS profile to access our AWS server
            //WriteProfile("default", "AKIA6LJNZIF7QCKALEUV", "DwEL+pPJTqpe6i+JtJmEoD3vdo28U+YDG/1FnXGD");

            /*
             * Got Cliet Config from 
             * https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/CodeSamples.DotNet.html
             */
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the US East 2 region.
            clientConfig.RegionEndpoint = RegionEndpoint.USEast2;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);

            Console.WriteLine("Debug 1");

            DynamoDBContext context = new DynamoDBContext(client);

            byte[] byteArray = { 1 };
            MemoryStream isAM = new MemoryStream(byteArray);

            Console.WriteLine("Debug 2");

            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "BusNum", new AttributeValue { N = busNum.ToString() } },
                { "IsAM", new AttributeValue { B = isAM } }
            };

            Console.WriteLine("Debug 3");

            // Create GetItem request
            GetItemRequest request = new GetItemRequest
            {
                TableName = "BusRoutes",
                Key = key,
            };

            Console.WriteLine("Debug 4");

            // Issue request

            GetItemResponse result;

            try
            {
                result = await client.GetItemAsync(request);
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("Unable to compute mean: {0}",
                           ((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
                return null;
            }

            //var result = await client.GetItemAsync(request);

            Console.WriteLine("Debug 5");

            // View response
            Dictionary<string, AttributeValue> item = result.Item;

            Console.WriteLine("Debug 6");

            float[] corrdinates = { float.Parse(item["driverLactionLat"].N), float.Parse(item["driverLocationLon"].N) };

            Console.WriteLine("Done Scanning DB");

            return corrdinates;
        }

        private void WriteProfile(string profileName, string keyId, string secret)
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

