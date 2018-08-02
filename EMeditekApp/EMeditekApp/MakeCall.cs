using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMeditekApp
{
    public class MakeCall : ContentPage

    {
        public MakeCall()
        {
            Entry numEntry = new Entry
            {
                Placeholder = "Contact number",
                Text = "",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Button call_btn = new Button
            {

                BackgroundColor = Color.FromRgb(102, 204, 102),
                Text = "Call",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            call_btn.Clicked += (sender, args) =>
            {
                try
                {
                    if (numEntry.Text == "")
                    {
                        DisplayAlert("Alert", "Specify the number to start the call.", "OK");
                    }
                    else
                    {
                        DependencyService.Get<IPhoneCall>().MakeQuickCall(numEntry.Text.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            };



            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    numEntry
                    ,call_btn
                }
            };

        }
    }
}
