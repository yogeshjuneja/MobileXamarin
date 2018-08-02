using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using Plugin.Messaging;

namespace EMeditekApp.Wellogo
{
   
    public partial class BloodBankDetail : ContentPage
    {
         
        public BloodBankDetail(BloodBankMainData objBloodBankMainData=null)
        {
            InitializeComponent();
            try
            {

                if (objBloodBankMainData != null)
                {
                    this.Title = objBloodBankMainData.name;
                    lblName.Text = objBloodBankMainData.name;
                    lblAddress.Text = objBloodBankMainData.address;
                    lblContact.Text = objBloodBankMainData.contact;


                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) =>
                    {
                        // handle the tap
                        var phoneDialer = CrossMessaging.Current.PhoneDialer;
                        if (phoneDialer.CanMakePhoneCall)
                        {
                            if (objBloodBankMainData.contact.ToString().Contains(","))
                            {
                                string[] _numbers = objBloodBankMainData.contact.ToString().Split(',');
                                string selected = await DisplayActionSheet("Dial to:", "Cancel", null, _numbers);
                                if (selected != "Cancel" && !string.IsNullOrEmpty(selected))
                                {
                                    phoneDialer.MakePhoneCall(selected.ToString());
                                }
                            }
                            else
                            {
                                phoneDialer.MakePhoneCall(objBloodBankMainData.contact);
                            }
                        }
  
                    };

                   
                    lblContact.GestureRecognizers.Add(tapGestureRecognizer);
                    lblPincode.Text = objBloodBankMainData.pincode;
                    lblCity.Text = objBloodBankMainData.city;
                    lblState.Text = objBloodBankMainData.state;
                }
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

   
     
        protected override bool OnBackButtonPressed()
        {
            try
            {
                Navigation.PopAsync(true);
                
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
            return false;
        }
    }
}