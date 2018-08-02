using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using EMeditekApp.Wellogo.Models;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace EMeditekApp.Wellogo
{
    public partial class BloodBanks : ContentPage
    {
        public string strState { get; set; }
        public string strCity { get; set; }
        public int intPage { get; set; }
        public BloodBankRootClass objBloodBankRootClass { get; set; }
        public BloodBankPageData objBloodBankPageData { get; set; }
        public List<BloodBankMainData> lstBloodBank { get; set; }
        public BloodBanks(string state = "", string city = "", int page = 1)
        {
            InitializeComponent();
            try
            {
                if (Device.RuntimePlatform == "iOS")
                {
                   // ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                    ToolbarItems.Add(new ToolbarItem("Filter", "", () =>
                    {
                        overlay.IsVisible = true;
                        Navigation.PushModalAsync( new NavigationPage(new BloodBankFilter(eKyorType.BloodBank, state == "" ? "Any" : state, city == "" ? "Any" : city)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                        overlay.IsVisible = false;
                    }));
                    //ToolbarItems.Add(new ToolbarItem("Add", "", () =>
                    //{
                    //    overlay.IsVisible = true;
                    //    Navigation.PushAsync( new NavigationPage(new AddBloodDoner()));
                    //    overlay.IsVisible = false;
                    //}));
                }
                if (Device.RuntimePlatform == "Android")
                {
                    ToolbarItems.Add(new ToolbarItem("Filter", "filter.png", () =>
                    {
                        Navigation.PushModalAsync(new NavigationPage(new BloodBankFilter(eKyorType.BloodBank, state == "" ? "Any" : state, city == "" ? "Any" : city)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                    }));
                    //ToolbarItems.Add(new ToolbarItem("Add Doner", "plus.png", () =>
                    //{
                    //    Navigation.PushModalAsync(new NavigationPage(new AddBloodDoner()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                    //    //  Navigation.PushAsync(new AddBloodDoner());
                    //}));
                 //   ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                }
                Load(state, city, page);
            }
            catch (Exception ex)
            {
               DependencyService.Get<IMessage>().LongAlert();
            }
        }
 
        private void lstBloodBanks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                BloodBankMainData objBloodBankMainData = (BloodBankMainData)e.Item;
                var page = new BloodBankDetail(objBloodBankMainData);
                Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void Load(string State, String City, int Page)
        {
            try
            {
                strState = State; strCity = City;
                if (lstBloodBank != null)
                {
                    LoadMore.IsVisible = true;
                }
                else
                {
                    overlay.IsVisible = true;
                }
                objBloodBankRootClass = await App.TodoManager.GetBloodBank(State == "Any" ? "" : State, City == "Any" ? "" : City, Page);
                objBloodBankPageData = objBloodBankRootClass.data;
                if (lstBloodBank != null)
                {
                    lstBloodBank.AddRange(objBloodBankPageData.data);
                    lstBloodBanks.ItemsSource = null;
                    lstBloodBanks.ItemsSource = lstBloodBank;
                }
                else
                {
                    lstBloodBank = objBloodBankPageData.data;
                    lstBloodBanks.ItemsSource = lstBloodBank;
                }
                overlay.IsVisible = false;
                LoadMore.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                LoadMore.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void lstBloodBanks_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ((ListView)sender).ItemsSource as IList;
            if (items != null && e.Item == items[Math.Max(items.Count - 1, 0)])
            {
                if (objBloodBankPageData.current_page < objBloodBankPageData.last_page)
                {
                    Load(strState == "Any" ? "" : strState, strCity == "Any" ? "" : strCity, objBloodBankPageData.current_page + 1);
                }
                //Load more items here  
            }
        }

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
            Navigation.PushAsync(new AddBloodDoner());
            overlay.IsVisible = false;
        }
    }
}