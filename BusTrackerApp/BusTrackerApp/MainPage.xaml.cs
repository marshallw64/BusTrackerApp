using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void studentButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StudentLoginPage());
        }

        private void parentButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ParentLoginPage());
        }
    }
}

