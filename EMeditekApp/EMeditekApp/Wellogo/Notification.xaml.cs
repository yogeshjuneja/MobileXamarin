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
    public partial class Notification : ContentPage
    {
        public AppNotification objAppNotification { get; set; }
        public List<AppNotificationDatum> lstlog { get; set; }
        public Notification()
        {
            InitializeComponent();
            this.Title = "Notification";
            GetNotofications();
        }

        async void GetNotofications(int page=1)
        {
         objAppNotification = await App.TodoManager.GetNotofications(page);
            if (lstlog != null)
            {
                lstlog.AddRange(objAppNotification.data.data);
                lstNotofication.ItemsSource = null;
                lstNotofication.ItemsSource = lstlog;
            }
            else
            {
                lstlog = objAppNotification.data.data;
                lstNotofication.ItemsSource = lstlog;
            }
             
        }

        private void lstNotofication_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var items = ((ListView)sender).ItemsSource as IList;
                if (items != null && e.Item == items[Math.Max(items.Count - 1, 0)])
                {
                    if (objAppNotification.data.current_page < objAppNotification.data.last_page)
                    {
                        lstNotofication.IsRefreshing = true;
                        GetNotofications(objAppNotification.data.current_page + 1);
                        lstNotofication.IsRefreshing = false;
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
            Navigation.PopModalAsync();
        }
    }
}