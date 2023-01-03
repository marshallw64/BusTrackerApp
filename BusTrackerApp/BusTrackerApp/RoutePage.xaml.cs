using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusTrackerApp
{
    public partial class RoutePage : ContentPage
    {
        public RoutePage()
        {
            InitializeComponent();
        }

        void postListView_ItemSelected(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RouteDetailsPage());
        }
    }
}

