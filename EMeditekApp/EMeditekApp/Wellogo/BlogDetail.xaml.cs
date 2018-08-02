using EMeditekApp.Wellogo.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{

    public partial class BlogDetail : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public BlogDetail(Int64 BlogID = 0, bool ismainpage = false)
        {

            try
            {
                InitializeComponent();
                //if (Device.RuntimePlatform == "iOS")
                //{
                //    if (!ismainpage)
                //    {
                //        ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //    }
                //}
                //if (Device.RuntimePlatform == "Android")
                //{
                //    if (!ismainpage)
                //    {
                //        ToolbarItems.Add(new ToolbarItem("Close", "down.png", () =>
                //        {
                //            if (ismainpage)
                //            {
                //                Navigation.PopAsync(true);
                //            }
                //            else
                //            {
                //                App.SetupRedirection(new Tabbedpage.TabbedPage1()); App.Current.MainPage = App.MasterDetailPage;
                //            }
                //        }));
                //    }
                //}

                if (InternetConnection)
                {
                    BindData(BlogID);
                }
                else
                {
                    AskForRetry(BlogID);
                }
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }

        }
        async void AskForRetry(Int64 BlogId)
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                BindData(BlogId);
            }
        }
        async void BindData(Int64 BlogID)
        {
            try
            {
                overlay.IsVisible = true;
                BlogFeatured objBlogFeatured = await App.TodoManager.GetBlogByID(BlogID);
                this.Title = objBlogFeatured.data.title;
                imgBlogImage.Source = objBlogFeatured.data.blog_photo;
                lblTitle.Text = objBlogFeatured.data.title;
                lblSubtititle.Text = objBlogFeatured.data.sub_title;
                lblDate.Text = Convert.ToDateTime(objBlogFeatured.data.created_at).ToString("dd MMMM,yyyy (hh:mm: tt)");

                lblContent.Text = DependencyService.Get<IParseHTML>().Parse(objBlogFeatured.data.content).ToString();

                List<Tag> lstTag = objBlogFeatured.data.tags;
                List<Category> lstCategory = objBlogFeatured.data.categories;
                Photo objPhoto = objBlogFeatured.data.photo;

                //listCategories.ItemsSource = lstCategory;
                foreach (Category c in lstCategory)
                {
                    Frame objFrame = new Frame() { OutlineColor = Color.Red, HasShadow = true, Padding = 1, BackgroundColor = Color.FromHex("#F8B4A5") };
                    objFrame.Content = new Label() { Text = c.name, TextColor = Color.Red };
                    stkCategoies.Children.Add(objFrame);
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.CommandParameter = c.name;
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        //Navigation.PopModalAsync(true);
                        //  App.Current.MainPage = new NavigationPage(new ViewBlog("", c.name, ""));
                        var page = new ViewBlog("", c.name, ""); 
                        Navigation.PushAsync(page);
                    };
                    objFrame.GestureRecognizers.Add(tapGestureRecognizer);
                }

                // listTags.ItemsSource = lstTag;
                foreach (Tag t in lstTag)
                {
                    Frame objFrame = new Frame() { OutlineColor = Color.Red, HasShadow = true, Padding = 1, BackgroundColor = Color.FromHex("#F8B4A5") };
                    objFrame.Content = new Label() { Text = t.name, TextColor = Color.Red };
                    stkTAgs.Children.Add(objFrame);

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.CommandParameter = t.name;
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        //    App.Current.MainPage = new NavigationPage(new ViewBlog("", "", t.name));
                        var page = new ViewBlog("", "", t.name);
                        Navigation.PushAsync(page);


                    };

                    objFrame.GestureRecognizers.Add(tapGestureRecognizer);
                }
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }

    }
}