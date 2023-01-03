using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusTrackerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapRouteTabbedPage : TabbedPage
    {
        public MapRouteTabbedPage()
        {
            InitializeComponent();
        }

        //when the settings button is clicked, it sends you to the settings page
        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }
    }
}
