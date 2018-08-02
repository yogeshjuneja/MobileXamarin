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
    public partial class RedeemDeal : ContentPage
    {
        #region Variables
        private int _Row = -1;
        private int _COlumn = -1;

        #endregion Variables
        #region Properties
        private int Row
        {
            get
            {
                // Get Next Row After Every Second Column
                return _Row = _Row == -1 ? 0 : _COlumn == 0 ? _Row + 1 : _Row;
            }
        }
        public int Column
        {
            get
            {  // Get Column Number in Sequence 1,2 for every row
                return _COlumn = _COlumn == 0 ? _COlumn + 1 : _COlumn == -1 ? 0 : _COlumn - 1;

            }
        }
        #endregion Properties
        public RedeemDeal(bool Ispopup = false)
        {
            InitializeComponent();
            if (Ispopup)
            {
                ToolbarItems.Add(new ToolbarItem("X", "", () => { Navigation.PopModalAsync(true); }));
            }
            BindDeals();
        }




        async void BindDeals()
        {
            try
            {



                Deals objRedeemDeal = await App.TodoManager.GetDeals();
                if (objRedeemDeal != null)
                {
                    if (objRedeemDeal.data != null)
                    {

                        foreach (DealsDatum d in objRedeemDeal.data.data)
                        {
                            #region DashBoardItems
                            Thickness objThickness = new Thickness(0, 0, 0, 0);
                            var TapGasture = new TapGestureRecognizer();
                            var frame = new Frame();
                            var stack = new StackLayout();
                            var Label = new Label() { FontSize = 14, TextColor = Color.FromHex("#000"), VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.None, Text = d.name };
                            BoxView bx = new BoxView() { HeightRequest = 1, HorizontalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 5), BackgroundColor = Color.Gray };
                            Label lbl = new Label() { Text = d.points.ToString() + " KOINS", TextColor = Color.FromHex("#f44337"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center };
                            Image I = new Image { Source = "http://squareboat.testing.kyor.com/deals/photo/" + d.photo.name, HeightRequest = 70, WidthRequest = 70, Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent };
                            var stack2 = new StackLayout();
                            stack2.Orientation = StackOrientation.Vertical;
                            stack2.Children.Add(I);
                            stack2.Children.Add(Label);
                            stack2.Children.Add(bx);
                            stack2.Children.Add(lbl);


                            stack.VerticalOptions = LayoutOptions.FillAndExpand;
                            stack.Orientation = StackOrientation.Vertical;

                            stack.Padding = objThickness;

                            TapGasture.Tapped += (s, e) => { Navigation.PushAsync(new RedeemSingleDeal(d)); };
                            stack.GestureRecognizers.Add(TapGasture);
                            stack.Children.Add(stack2);

                            frame.BackgroundColor = Color.FromHex("#f8f8f8");
                            frame.HasShadow = true;
                            frame.Content = stack;
                            LoginGrid.Children.Add(frame, Column, Row);

                            #endregion DashBoardItems

                        }

                    }

                }


            }
            catch (Exception)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }




        }

    }
}