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
    public partial class Social : TabbedPage
    {
        public LeaderBoardLogRootObject objLeaderBoardLogRootObject { get; set; }
        public List<LeaderBoardLogdata> lstlog { get; set; }
  
        public Social()
        {
            InitializeComponent();
            GetLeaderBoardLog();
            this.CurrentPageChanged += (object sender, EventArgs e) => {
                var i = this.Children.IndexOf(this.CurrentPage);
                if(i==1)
                {
                    BindData();
                    lstLeaderBoardLog.ItemsSource = null;
                }
                else
                {
                    GetLeaderBoardLog();
                    lstLeaderBoard.ItemsSource = null;
                }
            };
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
    }
}