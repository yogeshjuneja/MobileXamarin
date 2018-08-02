using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using System.IO;

using Xamarin.Forms;
using EMeditekApp.iOS;

using Foundation;
using UIKit;

[assembly: Dependency(typeof(PhoneCall_iOS))]
namespace EMeditekApp.iOS
{
    public class PhoneCall_iOS : IPhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            {
                NSUrl url = new NSUrl(string.Format(@"telprompt://{0}", PhoneNumber));
                UIApplication.SharedApplication.OpenUrl(url);
            }
            catch (Exception ex)
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = "iOS Exception";
                alert.AddButton("OK");
                alert.Message = ex.ToString();
                alert.Show();
            }
        }
    }

}