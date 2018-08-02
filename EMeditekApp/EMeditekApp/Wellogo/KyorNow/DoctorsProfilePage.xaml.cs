using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo.KyorNow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorsProfilePage : ContentPage
    {
        public DoctorsProfilePage()
        {
            InitializeComponent();
            BindingContext = this;

            //if (Device.RuntimePlatform == "iOS")
            //{
            //    ToolbarItems.Add(new ToolbarItem("<Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}
            //if (Device.RuntimePlatform == "Android")
            //{
            //    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}
            NavigationPage.SetHasNavigationBar(this, true);

        }
    }
}