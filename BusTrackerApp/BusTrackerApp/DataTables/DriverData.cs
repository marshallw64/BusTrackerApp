﻿using System;
using SQLite;
namespace BusTrackerApp.DataTables
{
	public class DriverData
	{
		//Stores the bus number of the bus that the driver is currently driving
		public int busNum { get; set; }

		//stores wether the driver is tring during the morning or afternoon
		//true = AM; false = PM
		public bool isAM { get; set; }

		[PrimaryKey]
		//Stores the password of the driver
		public string driverPassword { get; set; }
	}
}

