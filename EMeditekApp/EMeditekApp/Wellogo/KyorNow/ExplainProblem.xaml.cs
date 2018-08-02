using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo.KyorNow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplainProblem : ContentPage
    {
        public ExplainProblem()
        {
            InitializeComponent();
            BindingContext = this;

            //if (Device.RuntimePlatform == "iOS")
            //{
            //    ToolbarItems.Add(new ToolbarItem("<Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}
            //if (Device.RuntimePlatform == "Android")
            //{
            //    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}

            NavigationPage.SetHasNavigationBar(this, true); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            #region Pickers Items Control

            ObservableCollection<string> pickerlist = new ObservableCollection<string>();
            pickerlist.Add("Data One");
            pickerlist.Add("Data Two");
            pickerlist.Add("Data Three ");
            picker.ItemsSource = pickerlist;

            #endregion
        }


        #region Self And Someone else button effect control
        void buttonApperarence(bool IsSelf)
        {
            if (IsSelf)
            {
                SelfButton.TextColor = Color.FromHex("#f44337");
                SelfLine.BackgroundColor = Color.FromHex("#f44337");

                SomeoneElseButton.TextColor = Color.Gray;
                SomeoneElseLine.BackgroundColor = Color.Gray;

            }
            if(!IsSelf)
            {
                SelfButton.TextColor = Color.Gray;
                SelfLine.BackgroundColor = Color.Gray;

                SomeoneElseButton.TextColor = Color.FromHex("#f44337");
                SomeoneElseLine.BackgroundColor = Color.FromHex("#f44337");
            }

        }

        #endregion

        #region Gender button effect control
        void GenderbuttonApperarence(int gender)
        {

            MaleButton.TextColor = Color.Gray;
            FemaleButton.TextColor = Color.Gray;
            OtherButton.TextColor = Color.Gray;

            if (gender == 0)
            {
                FemaleButton.TextColor = Color.FromHex("#f44337");
            }
            if (gender == 1)
            {
                MaleButton.TextColor = Color.FromHex("#f44337");
            }
            if (gender == 2)
            {
                OtherButton.TextColor = Color.FromHex("#f44337");
            }

        }

        #endregion

        #region Buttons Controls

        private void SelfButton_Clicked(object sender, EventArgs e)
        {
            buttonApperarence(true); //  calling Self And Someone else button effect control


        }
        private void SomeoneElseButton_Clicked(object sender, EventArgs e)
        {
            buttonApperarence(false); //  calling Self And Someone else button effect control

        }


        private void FemaleButton_Clicked(object sender, EventArgs e)
        {
            GenderbuttonApperarence(0); // Gender effect

        }
        private void MaleButton_Clicked(object sender, EventArgs e)
        {
            GenderbuttonApperarence(1); // Gender effect

        }
        private void OtherButton_Clicked(object sender, EventArgs e)
        {
            GenderbuttonApperarence(2); // Gender effect


        }
        private void NextButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new DoctorsList()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
            //Navigation.PushModalAsync(new DoctorsList());
        }

        #endregion

    }

}