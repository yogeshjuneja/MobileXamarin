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
    public partial class RedeemSingleDeal : ContentPage
    {
        private DealsDatum deal { get; set; }
        public RedeemSingleDeal(DealsDatum d = null)
        {
            deal = d;
            InitializeComponent();
            BindDeal();
        }

        async void BindDeal()
        {
            try
            {
                if (deal != null)
                {
                    //   EarnPoints objEarnPoints = await App.TodoManager.GetSingleDeal(deal.partner_id, deal.id);
                    imagedeal.Source = "http://squareboat.testing.kyor.com/deals/photo/" + deal.photo.name;
                    lblMainText.Text = deal.name;
                    lblSubmaintext.Text = deal.description;
                    lblKoins.Text = "Order Now -" + deal.points.ToString() + " KOINS";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
            object obj = await App.TodoManager.RedeemDeal(deal.id);
            if (obj is RedeemDealResponse)
            {
                RedeemDealResponse objRedeemDealResponse = (RedeemDealResponse)obj;
                var existingPages = Navigation.NavigationStack.ToList();
                existingPages.RemoveAt(0); // Removing it because it is main page at 0
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            else
            {
                RedeemDealError objRedeemDealError = (RedeemDealError)obj;
                await DisplayAlert(objRedeemDealError.errors.message, objRedeemDealError.errors.validation.value[0], "OK");
            }
            overlay.IsVisible = false;

        }
    }
}