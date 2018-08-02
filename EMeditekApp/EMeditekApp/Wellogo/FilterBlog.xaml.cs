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
   
    public partial class FilterBlog : ContentPage
    {
        public FilterBlog(CategoryData objCategoryData=null)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == "iOS")
            {
                ToolbarItems.Add(new ToolbarItem("Close", "", () => { Navigation.PopModalAsync(true); }));
            }
            if (Device.RuntimePlatform == "Android")
            {
                ToolbarItems.Add(new ToolbarItem("Back", "down.png", () => { Navigation.PopModalAsync(true); }));
            }
            BindBlogCategories(objCategoryData);
        }

        async void BindBlogCategories(CategoryData objCategoryData=null)
        {
            try
            {
                overlay.IsVisible = true;
                Allcategories objAllcategories = await App.TodoManager.GetAllBlogCategories();
                pkrState.ItemsSource = objAllcategories.data;
                overlay.IsVisible = false;
                if (objCategoryData != null)
                {
                    foreach(CategoryData c in objAllcategories.data)
                    {
                        if(c.name== objCategoryData.name)
                        {
                            pkrState.SelectedItem = c;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        private void Apply_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                CategoryData objCategoryData = pkrState.SelectedItem as CategoryData;

                if(objCategoryData!=null)
                {
                   Navigation.PushAsync(new ViewBlog("", objCategoryData.name,""));
                   Navigation.PopModalAsync(true);
                }
                else
                {
                    Navigation.PopModalAsync(true);
                }

                
            }
            catch (Exception ex)
            {
                overlay.IsVisible = true;

                 DependencyService.Get<IMessage>().LongAlert();
            }
        }
    }
}