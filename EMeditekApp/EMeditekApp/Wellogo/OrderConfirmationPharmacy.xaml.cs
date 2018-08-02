using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
  
namespace EMeditekApp.Wellogo
{
   
    public partial class OrderConfirmationPharmacy : ContentPage
    {
        public Pharmacy _Pharmacy { get; set; }
        public List<PrescriptionPhotos> photos { get; set; }
        public OrderConfirmationPharmacy(Pharmacy objPharmacy=null)
        {

            try
            {
                InitializeComponent();
                _Pharmacy = objPharmacy;
                if (objPharmacy != null)
                {
                    BindData();
                }

            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
           
            //var tgr = new TapGestureRecognizer();
            //tgr.Tapped += (s, e) => AddMoreImages();
            //lblAddMore.GestureRecognizers.Add(tgr);
        }
   void CheckPhotos()
        {
                btnUpload.IsEnabled = _Pharmacy.data.photos.Count < 4;
        }
      async  void BindData()
        {
            try
            {
                CheckPhotos();
                overlay.IsVisible = true;
                lstPharmacy.ItemsSource = _Pharmacy.data.photos;
                
                lblName.Text = _Pharmacy.data.address_dump.name;
                lbladdress.Text= _Pharmacy.data.address_dump.address;
                lblState.Text = _Pharmacy.data.address_dump.state;
                lblCity.Text = _Pharmacy.data.address_dump.city;
                lblPin.Text = _Pharmacy.data.address_dump.pin;
                lblMobile.Text = _Pharmacy.data.address_dump.phone;
                GC.Collect();

                //StackLayout stkCloseButton;
                //Image img;
                //StackLayout stkImagesStack;
                //foreach (PrescriptionPhotos b in photos)
                //{
                //    stkCloseButton = new StackLayout() {HorizontalOptions= LayoutOptions.EndAndExpand };
                //    img = new Image() { Source = "cross.png", HeightRequest = 30, HorizontalOptions = LayoutOptions.EndAndExpand, WidthRequest = 30 };
                //    var tgr = new TapGestureRecognizer();
                //    tgr.Tapped += (s, e) => TapGestureRecognizer_Tapped(s, e);
                //    tgr.CommandParameter = b;
                //    img.GestureRecognizers.Add(tgr);
                //    stkCloseButton.Children.Add(img);

                //    stkImagesStack = new StackLayout() {Orientation=StackOrientation.Horizontal,VerticalOptions=LayoutOptions.CenterAndExpand };
                //    stkImagesStack.Children.Add(new Image() {HeightRequest=60,IsOpaque=true,WidthRequest=60,Source=b.url });
                //    stkImagesStack.Children.Add(new Label() {FontSize=15,Text= b.name });
                //    stkImagesStack.Children.Add(stkCloseButton);
                //}
                photos = _Pharmacy.data.photos;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                 
                GC.Collect();
                overlay.IsVisible = true;
                Pharmacy objPharmacy = await App.TodoManager.PharmacyPlaceOrder(_Pharmacy.data.id);
                if(objPharmacy.status=="success")
                {
                    await DisplayAlert("Order successfully placed", "Our partner retailer/pharmacist will contact you shortly to verify your order and update you with the relevant  information. Once confirmed , you will receive a formal order confirmation along with an estimated delivery time ", "OK");
                    App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1());
                    App.Current.MainPage = App.MasterDetailPage;
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert(objPharmacy.message);
                }


                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
      

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                 var a = await DisplayAlert("", "Are you sure", "Yes", "No");
                if(a==true)
                {
                    overlay.IsVisible = true;
                    dynamic obj = e;
                   
                    int photoid = obj.Parameter;
                    PharmacyPhotoDelete objPharmacyPhotoDelete = await App.TodoManager.PharmacyDeletePhoto(_Pharmacy.data.id, photoid);
                    int i = 0;
                    if (objPharmacyPhotoDelete.status == "success")
                    {
                        foreach (PrescriptionPhotos p in photos)
                        {
                            if (p.id == photoid)
                            {

                                photos.RemoveAt(i);
                                break;
                            }
                            i++;
                        }
                         lstPharmacy.ItemsSource = null;
                        lstPharmacy.ItemsSource =  photos;
                        CheckPhotos();
                    }

                    GC.Collect();
                    overlay.IsVisible = false;
                }
                
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();                
            }
        }

        private async void btnUpload_Clicked(object sender, EventArgs e)
        {
            GC.Collect();
            await Navigation.PushAsync(new UploadPrescription(_Pharmacy));
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            GC.Collect();
            Navigation.PopAsync();
        }

     
    }
}