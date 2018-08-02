using EMeditekApp.Wellogo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialNew : ContentPage
    {
        public LeaderBoardLogRootObject objLeaderBoardLogRootObject { get; set; }
        public List<LeaderBoardLogdata> lstlog { get; set; }
        public SocialNew()
        {
            InitializeComponent();
            lstLeaderBoardLog.IsRefreshing = true;
            GetLeaderBoardLog();
            BindData();
        //    GetKoins();
            lstLeaderBoardLog.IsRefreshing = false;
        }
        protected override void OnAppearing()
        {
            GetKoins();
        }
        async void GetKoins()
        {
            try
            {

                UserDetais objUserDetais = await App.TodoManager.UserDetais(App.id);
                if (objUserDetais.data != null)
                {

                    lblTotalKoins.Text= objUserDetais.data.points.ToString() + " KOINS";
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
            finally
            {

            }

        }
        async void BindData()
        {
            try
            {
                //overlay.IsVisible = true;
                LeaderboardData objLeaderboardData = await App.TodoManager.GetLeaderboardUsers();
                lstLeaderBoard.ItemsSource = objLeaderboardData.data.result;


               //overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.ToString(), "ok");
            }
        }
        async void GetLeaderBoardLog(int page=1)
        {
            try
            {
                 objLeaderBoardLogRootObject = await App.TodoManager.GetLeaderboardLog();
                if (lstlog != null)
                {
                    lstlog.AddRange(objLeaderBoardLogRootObject.data.data);
                    lstLeaderBoardLog.ItemsSource = null;
                    lstLeaderBoardLog.ItemsSource = lstlog;

                }
                else
                {
                    lstlog = objLeaderBoardLogRootObject.data.data;
                    lstLeaderBoardLog.ItemsSource = lstlog;
                }
                //lstLeaderBoardLog.ItemsSource = objLeaderBoardLogRootObject.data.data;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        private void lstLeaderBoardLog_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var items = ((ListView)sender).ItemsSource as IList;
                if (items != null && e.Item == items[Math.Max(items.Count - 1, 0)])
                {
                    if (objLeaderBoardLogRootObject.data.current_page < objLeaderBoardLogRootObject.data.last_page)
                    {
                        lstLeaderBoardLog.IsRefreshing = true;
                        GetLeaderBoardLog(objLeaderBoardLogRootObject.data.current_page+1);
                        lstLeaderBoardLog.IsRefreshing = false;
                    }
                    //Load more items here  
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            try
            {
               //App.SetupRedirection(new index());
               // App.Current.MainPage = App.MasterDetailPage;
 
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            tabOne.IsVisible = true;
            tabOneLine.BackgroundColor = Color.White;
            tabtwo.IsVisible = false;
            tabtwoLine.BackgroundColor = Color.FromHex("#f44337");
            this.Title = "Points History";
            GetKoins();

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            tabOne.IsVisible = false;
            tabOneLine.BackgroundColor = Color.FromHex("#f44337");
            tabtwo.IsVisible = true;
            tabtwoLine.BackgroundColor = Color.White;
            this.Title = "Leaderboard";

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RedeemDeal());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HowToEarnPoints());
        }
    }
}