using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
   
    public partial class HCAHServices : ContentPage
    {
        public HCAHServices()
        {
            InitializeComponent();
            try
            {
                //if (Device.RuntimePlatform == "iOS")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
                //if (Device.RuntimePlatform == "Android")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
                this.Title = "Home Health";
                BindData();
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void listHomeHealth_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var Item = e.Item as HCAH;
                string PageName = Item.name;
                var page =new HomeHealthForm(Item);
                Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        async void BindData()
        {
            try
            {
                overlay.IsVisible = true;
                HCAHData lstHomeHealthData = await App.TodoManager.HCAHServices();
                listHomeHealth.ItemsSource = lstHomeHealthData.data;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
    }
}