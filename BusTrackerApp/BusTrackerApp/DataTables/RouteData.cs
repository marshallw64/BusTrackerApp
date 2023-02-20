using System;
using SQLite;
using Xamarin.Forms.Maps;

namespace BusTrackerApp.DataTables
{
	public class RouteData
	{
		//Stores the number of the route which will be used to ID each route
		[PrimaryKey]
		public int busNum { get; set; }

		//Says whether the route is during the morning (true) or afternoon (false)
		public bool isAM { get; set; }

        //Stores the coordinates to draw the route on the map 
        public Position[] routeLineCords { get; set; }

        //Stores the coordinates for each bus stop to put on the map
        public Position[] busStopCords { get; set; }

		//Stores the current location of the driver 
		public Position driverLocation { get; set; }
    }
}

