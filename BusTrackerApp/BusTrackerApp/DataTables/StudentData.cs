using System;
using SQLite;

namespace BusTrackerApp.DataTables
{
	public class StudentData
	{
        //Stores the student ID of each student who rides the bus which will be used to identify each student
		[PrimaryKey]
		public int studentID { get; set; }

        //Stores the password of each student
        [MaxLength(20)]
        public string studentPassword { get; set; }

        //Stores the frist name of a student
		public string studentFirstName { get; set; }

        //Stores the last name of a student
        public string studentLastName { get; set; }

        //Stores the route number that the student takes in the morning
        public int studentRouteNumAM { get; set; }

        //Stores the route number that the student takes in the afternoon
        public int studentRouteNumPM { get; set; }
    }
}

