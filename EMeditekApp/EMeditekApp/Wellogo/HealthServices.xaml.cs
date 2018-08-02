using EMeditekApp.Wellogo.Models;
using FFImageLoading.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamd.ImageCarousel.Forms.Plugin.Abstractions;

namespace EMeditekApp.Wellogo
{

    public partial class HealthServices : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public Dictionary<string, int> dcCholestrol { get; set; }
        public Dictionary<string, int> dcSugar { get; set; }
        public Dictionary<string, double> dcBMI { get; set; }
        public Dictionary<string, int> dcBPsys { get; set; }
        public Dictionary<string, int> dcBPdys { get; set; }
        int i = 0;
        string[] MonthYear = new string[2];
        string Value = "";
        LinearAxis objLinearAxis = new LinearAxis();

        public HealthServices()
        {
            try
            {
                InitializeComponent();
                this.Title = "Home";
                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => OnLabelMedicalHistoryClicked();
                lblMedicalHistory.GestureRecognizers.Add(tgr);
                tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => OnLabelStartHRAClicked();
                lblStartNewHRA.GestureRecognizers.Add(tgr);
                if (InternetConnection)
                {
                    BiNDHRA();
                    CreateAreaChart();
                    ShowBlogs();
                    CheckIntegrateTracker();

                    BindUserDetails();
                }
                else
                {
                    AskForRetry();
                }
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }
       async void BindUserDetails()
        {
            try
            {
                overlay.IsVisible = true;
                UserDetais objUserDetais = await App.TodoManager.UserDetais(App.id);
                if (objUserDetais.data != null)
                {
                    lblName.Text = objUserDetais.data.name;
                    userprofile.Source = objUserDetais.data.photo_url;
                    lblCoins.Text = objUserDetais.data.points.ToString() + " KOINS";
                }
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
        async void AskForRetry()
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                BiNDHRA();
                CreateAreaChart();
                ShowBlogs();
            }
        }
        private async void CreateAreaChart()
        {
            try
            {
                List<OxyColor> objlstOxycolor = new List<OxyColor>() { OxyColor.FromRgb(156, 29, 2) };
                HealthStats objHealthStats = await App.TodoManager.GetHealthStats();
                //objHealthStats.data.cholesterol.data
                dcCholestrol = objHealthStats.data.cholesterol.data;
                int MaxValueCholestrol = objHealthStats.data.cholesterol.data.Count - 1;
                var plotModel = new PlotModel { LegendPosition = LegendPosition.TopCenter };
                plotModel.AxisTierDistance = 0;
                plotModel.PlotAreaBorderColor = OxyColors.Transparent;
                plotModel.DefaultColors = objlstOxycolor;

                #region CholestrolChart
                plotModel.Axes.Add(new LinearAxis()
                {
                    Position = AxisPosition.Bottom,
                    Title = "Month",
                    MinorTickSize = 0,
                    MajorStep = 1,
                    AbsoluteMinimum = 0,
                    AbsoluteMaximum = MaxValueCholestrol,
                    LabelFormatter = ValueAxisLabelFormatter,
                    AxislineStyle = LineStyle.Solid,
                });

                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    AbsoluteMaximum = 300,
                    AbsoluteMinimum = 0,
                    Title = "Value",
                    MinorTickSize = 0,
                    MajorStep = 50,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    MinimumMajorStep = 3,
                    AxislineStyle = LineStyle.Solid,
                });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Right, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Top, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });

                var series1 = new LineSeries { MarkerType = MarkerType.Circle, MarkerSize = 5, MarkerStroke = OxyColors.White, Title = "Total Cholestrol" };
                var series2 = new LineSeries { MarkerType = MarkerType.None, MarkerSize = 0, MarkerStroke = OxyColors.Transparent };
                series2.Points.Add(new DataPoint(2, 300));

                int KeyCount = 0;
                foreach (var a in dcCholestrol)
                {
                    series1.Points.Add(new DataPoint(KeyCount, a.Value));
                    KeyCount++;
                }
                plotModel.Series.Add(series2);
                plotModel.Series.Add(series1);
                series1.MouseDown += Series1_MouseDown;
                ChartCholesterol.Model = plotModel;
                #endregion

                #region SugarChart
                dcSugar = objHealthStats.data.sugar.data;
                MaxValueCholestrol = objHealthStats.data.sugar.data.Count - 1;
                plotModel = new PlotModel
                {
                    LegendPosition = LegendPosition.TopCenter,
                    DefaultColors = objlstOxycolor
                };
                plotModel.AxisTierDistance = 0;
                plotModel.PlotAreaBorderColor = OxyColors.Transparent;

                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Month",
                    MinorTickSize = 0,
                    MajorStep = 1,
                    AbsoluteMinimum = 0,
                    AbsoluteMaximum = MaxValueCholestrol,
                    LabelFormatter = ValueAxisLabelFormatterSugar,
                    AxislineStyle = LineStyle.Solid
                });
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    AbsoluteMaximum = 450,
                    AbsoluteMinimum = 0,
                    Title = "Value",
                    MinorTickSize = 0,
                    MajorStep = 50,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    MinimumMajorStep = 3,
                    AxislineStyle = LineStyle.Solid,
                });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Right, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Top, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                series1 = new LineSeries { MarkerType = MarkerType.Circle, MarkerSize = 5, MarkerStroke = OxyColors.White, Title = "Blood Sugar" };
                series2 = new LineSeries { MarkerType = MarkerType.None, MarkerSize = 0, MarkerStroke = OxyColors.Transparent };
                series2.Points.Add(new DataPoint(2, 450));
                KeyCount = 0;
                foreach (var a in dcSugar)
                {
                    series1.Points.Add(new DataPoint(KeyCount, a.Value));
                    KeyCount++;
                }
                plotModel.Series.Add(series2);
                plotModel.Series.Add(series1);
                ChartSugar.Model = plotModel;



                #endregion

                #region BMI
                dcBMI = objHealthStats.data.bmi.data;
                MaxValueCholestrol = objHealthStats.data.bmi.data.Count - 1;
                plotModel = new PlotModel { LegendPosition = LegendPosition.TopCenter, DefaultColors = objlstOxycolor };
                plotModel.AxisTierDistance = 0;
                plotModel.PlotAreaBorderColor = OxyColors.Transparent;
                //    plotModel.DefaultXAxis = OxyColor.FromRgb(156, 29, 2);


                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Month",
                    MinorTickSize = 0,
                    MajorStep = 1,
                    AbsoluteMinimum = 0,
                    AbsoluteMaximum = MaxValueCholestrol,
                    LabelFormatter = ValueAxisLabelFormatterBMI,
                    AxislineStyle = LineStyle.Solid

                });
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    AbsoluteMaximum = 50,
                    AbsoluteMinimum = 0,
                    Title = "Value",
                    MinorTickSize = 0,
                    MajorStep = 10,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    MinimumMajorStep = 3,
                    AxislineStyle = LineStyle.Solid
                });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Right, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Top, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                series1 = new LineSeries { MarkerType = MarkerType.Circle, MarkerSize = 5, MarkerStroke = OxyColors.White, Title = "BMI" };
                series2 = new LineSeries { MarkerType = MarkerType.None, MarkerSize = 0, MarkerStroke = OxyColors.Transparent };
                series2.Points.Add(new DataPoint(2, 50));
                KeyCount = 0;
                foreach (var a in dcBMI)
                {
                    series1.Points.Add(new DataPoint(KeyCount, Convert.ToInt32(a.Value)));
                    KeyCount++;
                }
                plotModel.Series.Add(series2);
                plotModel.Series.Add(series1);
                chartBMI.Model = plotModel;



                #endregion

                #region Blood Pressure
                dcBPsys = objHealthStats.data.blood_pressure_systolic.data;
                dcBPdys = objHealthStats.data.blood_pressure_diastolic.data;
                MaxValueCholestrol = objHealthStats.data.blood_pressure_systolic.data.Count - 1;

                #region systolic
                plotModel = new PlotModel { LegendPosition = LegendPosition.TopCenter, DefaultColors = objlstOxycolor };
                plotModel.AxisTierDistance = 0;
                plotModel.PlotAreaBorderColor = OxyColors.Transparent;
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Month",
                    MinorTickSize = 0,
                    MajorStep = 1,
                    AbsoluteMinimum = 0,
                    AbsoluteMaximum = MaxValueCholestrol,
                    LabelFormatter = ValueAxisLabelFormatterBMI,
                    AxislineStyle = LineStyle.Solid
                });
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    AbsoluteMaximum = 300,
                    AbsoluteMinimum = 0,
                    Title = "Value",
                    MinorTickSize = 0,
                    MajorStep = 50,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    MinimumMajorStep = 3,
                    AxislineStyle = LineStyle.Solid,
                });

                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Right, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                plotModel.Axes.Add(new LinearAxis() { Position = AxisPosition.Top, IsAxisVisible = false, AxislineColor = OxyColors.Transparent });
                series1 = new LineSeries() { MarkerType = MarkerType.Circle, MarkerSize = 5, MarkerStroke = OxyColors.White, Title = "Systolic" };
                series2 = new LineSeries() { MarkerType = MarkerType.Diamond, MarkerSize = 5, MarkerStroke = OxyColors.White, Title = "Diastolic" };
                var series3 = new LineSeries { MarkerType = MarkerType.None, MarkerSize = 0, MarkerStroke = OxyColors.Transparent };
                series3.Points.Add(new DataPoint(2, 300));
                KeyCount = 0;
                foreach (var a in dcBPsys)
                {
                    series1.Points.Add(new DataPoint(KeyCount, a.Value));
                    KeyCount++;
                }

                KeyCount = 0;
                foreach (var a in dcBPdys)
                {
                    series2.Points.Add(new DataPoint(KeyCount, a.Value));
                    KeyCount++;
                }
                plotModel.Series.Add(series1);
                plotModel.Series.Add(series2);
                plotModel.Series.Add(series3);
                chartBloodPressure.Model = plotModel;



                #endregion
                //    ObservableCollection<FileImageSource> objimage = new ObservableCollection<FileImageSource>() { new FileImageSource() { File = (FileImageSource)ImageSource.FromFile("book.png") },
                //        new FileImageSource() { File = (FileImageSource)ImageSource.FromFile("claim.png") },
                //        new FileImageSource() { File = (FileImageSource)ImageSource.FromFile("cross.png") },
                //         new FileImageSource() { File = (FileImageSource)ImageSource.FromFile("cloud.png") }
                //};


                //    cvimage.Images = objimage;


                #endregion

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        async void CheckIntegrateTracker()
        {
            try
            {
                overlay.IsVisible = true;
                Tracker objTracker = await App.TodoManager.GetClientTracker();
                if (objTracker != null)
                {
                    if (objTracker.data == null)
                    {
                        lblFitnessMessage.Text = "You havn't integrated any fitness tracker";
                        return;
                    }
                    if (objTracker.data.type == 1)
                    {

                        lblFitnessMessage.Text = "Fit Bit Integrated";
                    }
                    else
                    {

                        lblFitnessMessage.Text = "Google Fit";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }

        private void Series1_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            double a = (sender as LineSeries).InverseTransform(e.Position).X;
        }

        void alert()
        {
            DisplayAlert("Message", "swipe left", "OK");
        }
        private string ValueAxisLabelFormatter(double input)
        {
            i = 0;
            MonthYear = new string[2];
            Value = "";


            foreach (var a in dcCholestrol)
            {
                if (i == input)
                {
                    MonthYear = a.Key.ToString().Split('-');
                    Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthYear[1])).Substring(0, 3) + " " + MonthYear[0];
                    break;
                }
                i++;

            }
            return Value;
        }
        private string ValueAxisLabelFormatterSugar(double input)
        {
            i = 0;
            MonthYear = new string[2];
            Value = "";
            foreach (var a in dcSugar)
            {
                if (i == input)
                {
                    MonthYear = a.Key.ToString().Split('-');
                    Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthYear[1])).Substring(0, 3) + " " + MonthYear[0];
                    break;
                }
                i++;

            }
            return Value;
        }
        private string ValueAxisLabelFormatterBMI(double input)
        {
            i = 0;
            MonthYear = new string[2];
            Value = "";
            foreach (var a in dcBMI)
            {
                if (i == input)
                {
                    MonthYear = a.Key.ToString().Split('-');
                    Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthYear[1])).Substring(0, 3) + " " + MonthYear[0];
                    break;
                }
                i++;

            }
            return Value;
        }
        private string ValueAxisLabelFormatterBPsys(double input)
        {
            i = 0;
            MonthYear = new string[2];
            Value = "";
            foreach (var a in dcBPsys)
            {
                if (i == input)
                {
                    MonthYear = a.Key.ToString().Split('-');
                    Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthYear[1])).Substring(0, 3) + " " + MonthYear[0];
                    break;
                }
                i++;

            }
            return Value;
        }
        private void OnLabelStartHRAClicked()
        {
            try
            {
                Navigation.PushAsync(new HealthRiskAssesment(1));
                //App.Current.MainPage = new NavigationPage() { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void OnLabelMedicalHistoryClicked()
        {
            try
            {

               Navigation.PushAsync(new HealthRiskAssesment());
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void BiNDHRA()
        {
            try
            {
                overlay.IsVisible = true;
                DashBoardHRA objDashBoardHRA = await App.TodoManager.DashboardHRA();
                if (objDashBoardHRA.data.last_completed_hra_time != null & !string.IsNullOrEmpty(objDashBoardHRA.data.last_completed_hra_time))
                {
                    lblLastHRA.IsVisible = true;
                    lblHRAHeading.IsVisible = true;
                    lblLastHRA.Text = Convert.ToDateTime(objDashBoardHRA.data.last_completed_hra_time).ToString("dd MMMM, yyyy");

                }


                if (objDashBoardHRA.data.blood_pressure_systolic != null && objDashBoardHRA.data.blood_pressure_diastolic != null)
                {
                    lblBPHeading.IsVisible = true;
                    lblBP.IsVisible = true;
                    lblBP.Text = objDashBoardHRA.data.blood_pressure_systolic.ToString() + " / " + objDashBoardHRA.data.blood_pressure_diastolic.ToString();
                }

                if (objDashBoardHRA.data.weight_kgs != null)
                {
                    lblWeightHeading.IsVisible = true;
                    lblWeight.IsVisible = true;
                    lblWeight.Text = objDashBoardHRA.data.weight_kgs.ToString();
                }

                if (objDashBoardHRA.data.bmi != null)
                {
                    lblBMI.Text = objDashBoardHRA.data.bmi.ToString();
                    lblBMI.IsVisible = true;
                    lblBMIHeading.IsVisible = true;
                }

                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void ShowBlogs()

        {
            try
            {
                overlay.IsVisible = true;
                AllBlogs objAllBlogs = await App.TodoManager.GetAllBlogs("", "", "", 1);
                List<BlogData> lstBlogData = new List<BlogData>();
                lstBlogData = objAllBlogs.data.data;
                for (int i = 0; i < 3; i++)
                {
                    var imagestack = new StackLayout();
                    imagestack.HorizontalOptions = LayoutOptions.Fill;
                    imagestack.VerticalOptions = LayoutOptions.Fill;

                    CachedImage image = new CachedImage();
                    image.Source = lstBlogData[i].blog_photo;
                    imagestack.Margin = new Thickness(10, 5);
                    imagestack.HeightRequest = 70;
                    imagestack.WidthRequest = 70;
                    image.Aspect = Aspect.AspectFill;
                    imagestack.Children.Add(image);

                    var textstack = new StackLayout();
                    textstack.Orientation = StackOrientation.Vertical;

                    var labelblogtext = new Label();
                    labelblogtext.Text = lstBlogData[i].title;

                    var labeldate = new Label();
                    labeldate.Text = lstBlogData[i].created_at.ToString("dd MMM,yyyy (hh:mm tt)");
                    textstack.Children.Add(labelblogtext);
                    textstack.Children.Add(labeldate);

                    var MainStack = new StackLayout();
                    MainStack.Orientation = StackOrientation.Horizontal;
                    MainStack.Children.Add(imagestack);
                    MainStack.Children.Add(textstack);


                    var outerstack = new StackLayout();
                    outerstack.Orientation = StackOrientation.Horizontal;
                    outerstack.HorizontalOptions = LayoutOptions.FillAndExpand;
                    outerstack.IsClippedToBounds = true;
                    outerstack.Children.Add(MainStack);
                    Blogslack.Children.Add(outerstack);
                    var gesture = new TapGestureRecognizer();
                    gesture.CommandParameter = lstBlogData[i].id;
                    gesture.Tapped += (s, e) =>
                    {
                        dynamic obj = e;
                        int param = obj.Parameter;

                 Navigation.PushAsync( new BlogDetail(param));
                    };
                    outerstack.GestureRecognizers.Add(gesture);

                }
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {


            }


        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new FitnessTracker());
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewBlog());
        }
        protected override void OnAppearing()
        {
            BindUserDetails();
        }
    }



    public class Zoo
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}