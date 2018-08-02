using EMeditekApp.iOS;
using EMeditekApp.Wellogo.Models;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
[assembly: Dependency(typeof(OpenApp))]
namespace EMeditekApp.iOS
{
    public class OpenApp :IOpenApp
    {
        public void OpenAApplication(string PackageName, string IOSUrl = null, string AndroidUrl = null)
        {
            try
            {

                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(IOSUrl));
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IMessage>().LongAlert();
                }


            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }

        }
    }
}
