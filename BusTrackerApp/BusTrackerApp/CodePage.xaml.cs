using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    //Takes the generated 4-digit code and compares it to the entered one. If they match, it saves the entered email and password into the database for use in loging in, and sends the user to the MapPage
    public partial class CodePage : ContentPage
    {
        public CodePage()
        {
            InitializeComponent();
        }

        public CodePage(int code)
        {
            InitializeComponent();
        }
    }
}

