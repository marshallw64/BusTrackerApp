using System;
using Amazon.DynamoDBv2.DataModel;
using Xamarin.Forms.Maps;

namespace BusTrackerApp.DataTables
{
	public class AWSConnection
	{
        [DynamoDBTable("BusRoutes")]
        public class BusRoute
        {
            [DynamoDBHashKey]    // Hash key.
            public int BusNum { get; set; }
            public bool IsAM { get; set; }
            public Position[] routeLineCords { get; set; }
            public Position[] busStopCords { get; set; }
            public Position driverLocation { get; set; }
        }
    }
}

