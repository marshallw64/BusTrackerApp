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
    }
}
