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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HowToEarnPoints : ContentPage
    {
        public HowToEarnPoints()
        {
            InitializeComponent();
            EarnpointsDetails();
        }

        async void EarnpointsDetails()
        {
            lstEarn.IsRefreshing = true;
            EarnPoints objEarnPoints =await  App.TodoManager.HowToEarnPoints();
            lstEarn.ItemsSource = objEarnPoints.data;
            lstEarn.IsRefreshing = false;
            // objEarnPoints.data
        }
    }
}