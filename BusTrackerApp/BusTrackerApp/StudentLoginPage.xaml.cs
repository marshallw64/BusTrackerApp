﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class StudentLoginPage : ContentPage
    {
        public StudentLoginPage()
        {
            InitializeComponent();
        }

        private void studentLogin_Clicked(object sender, EventArgs e)
        {
            bool isIDEmpty = string.IsNullOrEmpty(IdEntry.Text);

            if (isIDEmpty) 
            {
              
            }
            else
            {
                Navigation.PushAsync(new MapPage());
            }
        }
    }
}

