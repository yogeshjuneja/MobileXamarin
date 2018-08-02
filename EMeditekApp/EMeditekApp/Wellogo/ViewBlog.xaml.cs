using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using System.Text.RegularExpressions;

namespace EMeditekApp.Wellogo
{
   
    public partial class ViewBlog : ContentPage
    {
        public bool isBusy { get; set; }

        public BlogData objBlogData { get; set; }


        public ViewBlog(string search="", string category="", string tag="", int page=1)
        {
            InitializeComponent();
            try
            {
                if (Device.RuntimePlatform == "iOS")
                {
                    ToolbarItems.Add(new ToolbarItem("Filter", "", () =>
                    {
                        CategoryData objCategoryData = new CategoryData();
                        objCategoryData.name = category;
                        Navigation.PushModalAsync(new NavigationPage(new FilterBlog(objCategoryData)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                    }));

                //    ToolbarItems.Add(new ToolbarItem("       Home", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                }
                if (Device.RuntimePlatform == "Android")
                {
                   // ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                    ToolbarItems.Add(new ToolbarItem("Filter", "filter.png", () =>
                    {
                        CategoryData objCategoryData = new CategoryData();
                        objCategoryData.name = category;
                        Navigation.PushModalAsync(new NavigationPage(new FilterBlog(objCategoryData)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                    }));
                }
                BindFeaturesBlog();
                BindAllBlogs(search,category,tag,page);
            }
            catch (Exception ex)
            {
               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            isBusy = false;
           
        }
        

      async  void BindFeaturesBlog()
        {
            try
            {
                overlay.IsVisible = true;
                BlogFeatured objBlogFeatured = await App.TodoManager.FeturedBlog();
                objBlogData = objBlogFeatured.data;
                Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                lblCOntent.Text = DependencyService.Get<IParseHTML>().Parse(objBlogFeatured.data.title).ToString();
                lblShortContent.Text = DependencyService.Get<IParseHTML>().Parse(objBlogFeatured.data.sub_title).ToString();
                //lblCOntent.Text = objBlogFeatured.data.title;
                //lblShortContent.Text = objBlogFeatured.data.sub_title;
                lblCreatedAt.Text= Convert.ToDateTime(objBlogFeatured.data.created_at).ToString("dd MMMM, yyyy (hh:mm tt)");
                  imgFeature.Source =  objBlogFeatured.data.blog_photo;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.CommandParameter = objBlogFeatured.data;
                tapGestureRecognizer.Tapped += (s, e) => {
                    dynamic obj = e;
                    Navigation.PushAsync(new BlogDetail(((BlogData)obj.Parameter).id, true));
                    //if (Device.RuntimePlatform == "iOS")
                    //{
                    //    Navigation.PushAsync(page);
                    //}
                    //else
                    //{
                    //    Navigation.PushModalAsync(page, true);
                    //}
                };
                FeaturedBlogStack.GestureRecognizers.Add(tapGestureRecognizer);
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
               overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void BindAllBlogs(string search , string category , string tag, int page)
        {
            try
            {
                overlay.IsVisible = true;
                Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                overlay.IsVisible = true;
                AllBlogs objAllBlogs = await App.TodoManager.GetAllBlogs(search,category,tag,page);

                listAllBlogs.ItemsSource = objAllBlogs.data.data;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }

        }
        private void ListViewBlog_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (isBusy)
                    return;
                isBusy = true;
                BlogData objBlogData = (BlogData)e.Item;
                Navigation.PushAsync(new BlogDetail(objBlogData.id,true),true);
                //if (Device.RuntimePlatform == "iOS")
                //{
                //    Navigation.PushAsync(page);
                //}
                //else
                //{
                //    Navigation.PushAsync(page, true);
                 
                //}

            }
            catch (Exception ex)
            {
                isBusy = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private async void Entry_Completed(object sender, EventArgs e)
        {
            try
            {
                overlay.IsVisible = true;
                listAllBlogs.ItemsSource = null;
                if (txtSearch.Text=="")
                {
                    lblFetured.IsVisible = true;
                    stkFeatured.IsVisible = true;
                    BindFeaturesBlog();
                    BindAllBlogs("", "", "",1);
                }
                else
                {
                    lblFetured.IsVisible = false;
                    stkFeatured.IsVisible = false;
                    BindAllBlogs(txtSearch.Text.Trim(), "", "", 1);
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