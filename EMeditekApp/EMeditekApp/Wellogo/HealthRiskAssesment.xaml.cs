using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using FormsPlugin.Iconize;


namespace EMeditekApp.Wellogo
{
   
    public partial class HealthRiskAssesment : ContentPage
    {

        public HealthRiskAssesment(int IsNewHRA = 0, int HRACompletedid = 0)
        {
            InitializeComponent();
            try
            {
                if (Device.RuntimePlatform == "iOS")
                {
                    ToolbarItems.Add(new ToolbarItem("+ Add   ", "", () =>
                    {
                        NewHRA();
                    })); 
                    //ToolbarItems.Add(new ToolbarItem("      < Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; })
                    //{
                    //    //Icon = "back.png", Priority = 0, Order = ToolbarItemOrder.Primary
                    //});

                    //{
                    //    Icon = "plus.png",Priority=0, Order = ToolbarItemOrder.Primary }
                    //);
                }
                if (Device.RuntimePlatform == "Android")
                {
                    //ToolbarItems.Add(new ToolbarItem("Home", "back.png", () =>
                    //{ App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));


                    ToolbarItems.Add(new ToolbarItem("Add HRA", "plus.png", () =>
                    {
                        NewHRA();
                    }));
                }
                BindHRA();

                if (IsNewHRA == 1)
                {
                    NewHRA();
                }
                else if (HRACompletedid > 0)
                {
                    Report(HRACompletedid);
                }

            }
            catch (Exception ex)
            {
               DependencyService.Get<IMessage>().LongAlert();
            }
        }
         
        async void BindHRA()
        {
            try
            {
                overlay.IsVisible = true;
                HealthRiskAssesmentList objHealthRiskAssesmentList = await App.TodoManager.GetHealthAssesment();
                lstHealthRiskAssesment.ItemsSource = objHealthRiskAssesmentList.lstHealthRiskAssesment;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void Report(int id)
        {
            overlay.IsVisible = true;
            await DownloadHraReport(id);
            overlay.IsVisible = false;
        }
        async void NewHRA()
        {
            try
            {
                
                var answer = await DisplayAlert("New HRA!", "This will create a new HRA for you. Please note that you can not delete an HRA once it is created.", "OK", "Cancel");
                if (answer)
                {
                    overlay.IsVisible = true;
                    HRAModel objHRAModel = await App.TodoManager.NewHRA();
                    await Navigation.PushAsync(new HRA_Steps.HRAStep1(objHRAModel.data.id));
                }
                //else
                //{
                //    await Navigation.PushAsync(new HRA_Steps.HRAStep1(0)); // Comment after testing
                //}
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
     public   async Task<String> DownloadHraReport(int ID, bool Alert = true)
        {
            try
            {
              
                if (Alert)
                {
                    overlay.IsVisible = true;
                    await DisplayAlert("HRA Completed!", "Great! The report for this HRA will now be downloaded. You can also download the report later manually.", "OK");
                }
              
                byte[] byteResponse = await App.TodoManager.DownloadHRAReport(ID);
              


                if(byteResponse!=null)
                {
                    string path = DependencyService.Get<ISQLite>().SavePdfFile(byteResponse);
                    return path;
                }
                else
                {

                    DependencyService.Get<IMessage>().ShortAlert("You must complete all steps of HRA to view the report");
                    return "";
                }

              
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert("No Application Available to View PDF");
                return "";
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }



        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                dynamic obj = e;
                int hraID = (int)obj.Parameter;
                overlay.IsVisible = true;
                await Navigation.PushAsync(new HRA_Steps.HRAStep1(hraID));
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
              
                dynamic obj = e;
                int hraID = (int)obj.Parameter;
                overlay.IsVisible = true;
                await  DownloadHraReport(hraID,false);
               
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
    }

}