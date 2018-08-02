using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo.KyorNow
{
   
    public partial class DoctorsList : ContentPage
    {
        public DoctorsList()
        {
            InitializeComponent();
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

        protected override bool OnBackButtonPressed()
        {
            try
            {
                //App.SetupRedirection(new index());
                //App.Current.MainPage = App.MasterDetailPage;
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            App.Current.MainPage = new NavigationPage(new DoctorsProfilePage()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
            //Navigation.PushModalAsync(new DoctorsProfilePage());
        }
    }
}