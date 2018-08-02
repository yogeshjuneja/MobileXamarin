using EMeditekApp.Wellogo;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EMeditekApp
{
    public partial class MenuPage : ContentPage
    {

        const string ImagePath = "http://emeditek.in/document/Photo/";
        //const string ImagePath = "http://192.168.1.206/document/Photo/";
        public MenuPage()
        {

        
            //ScrollView scroll = new ScrollView();
            //Content = scroll;
            StackLayout stackGray = new StackLayout() { BackgroundColor = Color.Transparent };//#c09335 09f
            stackGray.Orientation = StackOrientation.Vertical;
            stackGray.HorizontalOptions = LayoutOptions.FillAndExpand;
            stackGray.HeightRequest = 150;
            //Image imgBack = new Image { Source = "Pattern.jpg", Aspect = Aspect.Fill, HeightRequest = 150, WidthRequest = 400 };
            AbsoluteLayout absMain = new AbsoluteLayout { };
           // absMain.Children.Add(imgBack);
            AbsoluteLayout absInner = new AbsoluteLayout
            {
            };
            StackLayout stackInner = new StackLayout() { };
            stackGray.Orientation = StackOrientation.Vertical;
            stackGray.Padding = new Thickness(0, 10, 0, 10);
            stackGray.HorizontalOptions = LayoutOptions.FillAndExpand;
            string strPath = string.Empty;
            //CircleImage imgEmpPic = new CircleImage()
            //{
            //    Source = App.KyorData != null  ? App.KyorData.photo_url : "",
            //    //Source = "InsuredDetails1.png",
            //    Margin = new Thickness(10, 20, 0, 0),
            //    WidthRequest = 50,
            //    HeightRequest = 50,
            //    HorizontalOptions = LayoutOptions.StartAndExpand,
            //    BorderColor = Color.FromHex("#FFF"),
            //    BorderThickness = 1,
            //    Aspect = Aspect.AspectFit
            //};

            Image corporateImage = new Image()
            {
                Source = App.CorporateImageURl != null ? App.CorporateImageURl : "",
                HeightRequest = 160,
                WidthRequest = 290,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 0, 20, 5),
            };
            //Label lblName = new Label() { Text = App.Name, FontSize = 18, TextColor = Color.FromHex("#FFF"), Margin = new Thickness(20, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            //Label lblDesignation = new Label() { Text = App.Designation, FontSize = 16, TextColor = Color.FromHex("#FFF"), Margin = new Thickness(20, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            Label lblName = new Label() { Text = App.KyorData!=null? App.KyorData.name:"", FontSize = 16, TextColor = Color.FromHex("#000"), Margin = new Thickness(10, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            Label lblDesignation = new Label() { Text = App.KyorData != null ? Convert.ToString(App.KyorData.designation):"", FontSize = 14, TextColor = Color.FromHex("#000"), Margin = new Thickness(10, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            //stackInner.Children.Add(imgEmpPic);
            //stackInner.Children.Add(lblName);
            //stackInner.Children.Add(lblDesignation);
            stackGray.Children.Add(corporateImage);
            absInner.Children.Add(stackInner);
            absMain.Children.Add(absInner);
            stackGray.Children.Add(absMain);

            StackLayout stackMain = new StackLayout() { BackgroundColor = Color.FromHex("#f8f8f8"), Margin = new Thickness(10, 10, 0, 0) };//#024689
            stackMain.Orientation = StackOrientation.Vertical;
            stackMain.HorizontalOptions = LayoutOptions.StartAndExpand;
            stackMain.VerticalOptions = LayoutOptions.StartAndExpand;

            #region "Profile"

            var gestureRecognizerLanding = new TapGestureRecognizer();
            gestureRecognizerLanding.Tapped += (s, e) =>
            {
                try
                {
                   App.Current.MainPage = new NavigationPage(new Profile()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                catch (Exception ex)
                {

                }
            };

            StackLayout stackMainLanding = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stackLanding = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainLanding.GestureRecognizers.Add(gestureRecognizerLanding);
            stackLanding.GestureRecognizers.Add(gestureRecognizerLanding);
            Label lblLanding = new Label() { FontSize = 14, Text = "Profile", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#f8f8f8"), Margin = new Thickness(10, 6, 0, 0) };
            Image imgLanding = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "profile.png",
                HeightRequest = 35f,
                WidthRequest = 35f
            };
            stackLanding.Children.Add(imgLanding);
            stackLanding.Children.Add(lblLanding);
            stackMain.Children.Add(stackLanding);
            stackMain.Children.Add(stackMainLanding);
            StackLayout stackBorderLanding = new StackLayout() { BackgroundColor = Color.FromHex("#000"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            stackMain.Children.Add(stackBorderLanding);
            #endregion
            #region "Change Password"
            var gestureRecognizerHome = new TapGestureRecognizer();
            gestureRecognizerHome.Tapped += (s, e) =>
           {
               try
               {
                   App.Current.MainPage = new NavigationPage(new ChangePassword()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
               }
               catch (Exception ex)
               {


               }
           };

            StackLayout stackMainHomeDB = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stack1 = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainHomeDB.GestureRecognizers.Add(gestureRecognizerHome);
            stack1.GestureRecognizers.Add(gestureRecognizerHome);
            Label lblHome = new Label() { FontSize = 14, Text = "Change Password", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#f8f8f8"), Margin = new Thickness(10, 6, 0, 0) };
            Image imgHome = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "insureddetailsnew.png",
                HeightRequest = 35f,
                WidthRequest = 35f
            };
            stack1.Children.Add(imgHome);
            stack1.Children.Add(lblHome);
            stackMain.Children.Add(stack1);
            stackMain.Children.Add(stackMainHomeDB);
            StackLayout stackBorderReq = new StackLayout() { BackgroundColor = Color.FromHex("#000"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            stackMain.Children.Add(stackBorderReq);

            #endregion

            #region "Redeem Koins"
              gestureRecognizerHome = new TapGestureRecognizer();
            gestureRecognizerHome.Tapped += (s, e) =>
            {
                try
                {
                   /// Navigation.PushAsync(new RedeemDeal());
                    Navigation.PushModalAsync(new NavigationPage(new RedeemDeal(true)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                  //  App.Current.MainPage = new NavigationPage(new ChangePassword()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                catch (Exception ex)
                {


                }
            };

              stackMainHomeDB = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
              stack1 = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainHomeDB.GestureRecognizers.Add(gestureRecognizerHome);
            stack1.GestureRecognizers.Add(gestureRecognizerHome);
              lblHome = new Label() { FontSize = 14, Text = "Reedem Koins", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#f8f8f8"), Margin = new Thickness(10, 6, 0, 0) };
              imgHome = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "insureddetailsnew.png",
                HeightRequest = 35f,
                WidthRequest = 35f
            };
            stack1.Children.Add(imgHome);
            stack1.Children.Add(lblHome);
            stackMain.Children.Add(stack1);
            stackMain.Children.Add(stackMainHomeDB);
              stackBorderReq = new StackLayout() { BackgroundColor = Color.FromHex("#000"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            stackMain.Children.Add(stackBorderReq);

            #endregion

            #region "Logout"

            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.Tapped += (s, e) =>
            {
                App.Database.Logout();
                object response = Task.Run(() => App.TodoManager.Logout());
                App.Current.MainPage = new SignInPage();
                //Application.Current.Properties.Remove("EmployeeId");
                //App.Current.MainPage = new SignInPage();
            };

            StackLayout stackMainLogout = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stackLogout = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainLogout.GestureRecognizers.Add(gestureRecognizer);
            stackLogout.GestureRecognizers.Add(gestureRecognizer);
            Label lblLogout = new Label() { FontSize = 14, Text = "Logout", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#f8f8f8"), Margin = new Thickness(10, 6, 0, 0) };
            Image imgLogout = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "exit.png",
                HeightRequest = 35f,
                WidthRequest = 35f
            };
            stackLogout.Children.Add(imgLogout);
            stackLogout.Children.Add(lblLogout);
            stackMain.Children.Add(stackLogout);
            stackMain.Children.Add(stackMainLogout);

            #endregion

            ScrollView scroll = new ScrollView()
            {
                Content = stackMain
            };
            
            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform<int>(20, 0, 0), 0, 0),
                Children = { stackGray, scroll }
            };
            Content.BackgroundColor = Color.FromHex("#f8f8f8");            //c09335
            Title = "Menu";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            //Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
            Icon = "menu.png";
        }

       
    }
}
