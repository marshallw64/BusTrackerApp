using System;
using SQLite;
namespace BusTrackerApp.DataTables
{
	public class ParentData
	{
		//stores the email of a parent of a child that rides the bus which will be used to ID each parent
		[PrimaryKey, MaxLength(25)]
		public string parentEmail { get; set; }

		//Stores the password of a parent
		[MaxLength(20)]
		public string parentPassword { get; set; }

		//Stores the student ID of the child of the parent
		public int childStudentID { get; set; }

		//Stores the route number of the route the child takes in the morning
		public int studentRouteNumAM { get; set; }

        //Stores the route number of the route the child takes in the afternoon
        public int studentRouteNumPM { get; set; }
    }
}

