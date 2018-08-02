using EMeditekApp.Wellogo.Models;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace EMeditekApp.Wellogo
{

    public partial class UploadPrescription : ContentPage
    {
        private List<byte[]> lstbase64images { get; set; }
        private List<string> lstimagesname { get; set; }
        private Pharmacy objPharmacy { get; set; }
        public UploadPrescription(Pharmacy _Pharmacy = null)
        {
            InitializeComponent();
            try
            {
                //if (Device.RuntimePlatform == "iOS")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
                //if (Device.RuntimePlatform == "Android")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
                objPharmacy = _Pharmacy;
                if (objPharmacy != null)
                {
                    if (objPharmacy.data.photos.Count >= 4)
                    {
                        btnUpload.IsEnabled = false;
                    }
                    else
                    {
                        btnUpload.IsEnabled = true;
                    }
                }
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += Tgr_Tapped;
                lblschedule.GestureRecognizers.Add(tgr);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ScheduleX());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool valid = false;
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (storageStatus != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] {Permission.Storage});
                    storageStatus = results[Permission.Storage];
                }
                else
                {
                    valid = true;
                }
                if ((storageStatus == PermissionStatus.Granted) | valid)
                {
                    GC.Collect();
                    overlay.IsVisible = true;
                    int Maximages = 4;
                    if (objPharmacy != null)
                    {
                        Maximages = 4 - objPharmacy.data.photos.Count;
                    }
                    DependencyService.Get<IGallery>().OpenGallery(Maximages);
                    MessagingCenter.Subscribe<App, string>((App)Xamarin.Forms.Application.Current, "imagevalidation", (app, message) =>
                    {
                        DisplayAlert("Validation", message, "Cancel");
                        lstbase64images = null;
                        lblFileCount.Text = "No File Choosen";
                    });
                    MessagingCenter.Subscribe<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (app, list) =>
                    {
                        lstbase64images = list;
                        lblFileCount.Text = list.Count + " Images Selected";
                    });
                    MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedName", (app, list) =>
                    {
                        lstimagesname = list;
                    });
                    MessagingCenter.Subscribe<App, string>((App)Xamarin.Forms.Application.Current, "exception", (app, list) =>
                    {
                        DependencyService.Get<IMessage>().LongAlert();
                    });
                    overlay.IsVisible = false;
                    #region Commented
                    //FileData filedata = await CrossFilePicker.Current.PickFile();
                    //byte[] data = filedata.DataArray;
                    //string name = filedata.FileName;


                    // ((Activity)this.).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photos"), IMAGES_SELECTED);

                    // List<byte[]> _images = new List<byte[]>();


                    //MessagingCenter.Subscribe<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
                    //{
                    //    foreach (byte[] image in images)
                    //    {
                    //        Image newImage = new Image();
                    //        newImage.Source = ImageSource.FromStream(() => new MemoryStream(image));
                    //        //stackImages.Children.Add(newImage);  //Stacklayout that holds images
                    //        _images.Add(image);
                    //    }
                    //});


                    //var imageIntent = new Intent();
                    //imageIntent.SetType("image/*");
                    //imageIntent.SetAction(Intent.ActionGetContent);
                }
                #endregion
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
         
       

        private async void btnUpload_Clicked(object sender, EventArgs e)
        {
            try
            {
                int MaxImageselected;
                if (lstbase64images != null)
                {
                    overlay.IsVisible = true;

                    if (objPharmacy != null && objPharmacy.data != null)
                    {
                        MaxImageselected = 4 - objPharmacy.data.photos.Count;
                        if (lstbase64images.Count <= MaxImageselected)
                        {
                            objPharmacy = await App.TodoManager.UploadPrescription(lstbase64images, lstimagesname, objPharmacy.data.id);
                        }
                        else
                        {
                            await DisplayAlert("Server Error", "You can select only 4 images from which " + objPharmacy.data.photos.Count + "  has selected", "Cancel");
                        }
                    }
                    else
                    {
                        objPharmacy = await App.TodoManager.UploadPrescription(lstbase64images, lstimagesname);
                    }

                    if (objPharmacy.status == "success")
                    {
                        await Navigation.PushAsync(new MedicineDeleiveryDetails(objPharmacy));
                    }
                    else
                    {
                        await DisplayAlert("Server Error", "Sorry there is someissue while uploading images please try again later", "Cancel");
                    }
                }
                else
                {

                    DependencyService.Get<IMessage>().LongAlert("Please Select Images");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
            overlay.IsVisible = false;
        }


    }
}




