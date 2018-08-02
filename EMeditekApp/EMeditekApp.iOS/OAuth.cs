using EMeditekApp.Wellogo;
using System.Text;
using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.OAuth;
using System.Threading.Tasks;
using Xamarin.Auth;
using Foundation;
using UIKit;
using EMeditekApp.iOS;

[assembly: Dependency(typeof(OAuth))]

namespace EMeditekApp.iOS
{
    class OAuth : IOAuth
    {
        public void GetAuthoriseToken( int type)
        {

            #region Comented
            //StringBuilder objStringBuider = new StringBuilder();
            //objStringBuider.Append("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=791286977751-b6mlauvoiuns4sq4fkt8c52jh10g48q0.apps.googleusercontent.com&redirect_uri=http://localhost");
            //objStringBuider.Append("&scope=https://www.googleapis.com/auth/fitness.activity.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.activity.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_glucose.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_glucose.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_pressure.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_pressure.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body_temperature.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body_temperature.write");

            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.location.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.location.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.nutrition.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.nutrition.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.oxygen_saturation.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.oxygen_saturation.write");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.reproductive_health.read");
            //objStringBuider.Append(" https://www.googleapis.com/auth/fitness.reproductive_health.write");

            #endregion

            // string[] objScopes = new string[] {
            //     "https://www.googleapis.com/auth/fitness.activity.read",
            //     "https://www.googleapis.com/auth/fitness.activity.write",
            //     "https://www.googleapis.com/auth/fitness.blood_glucose.read",
            // "https://www.googleapis.com/auth/fitness.blood_glucose.write",
            // "https://www.googleapis.com/auth/fitness.blood_pressure.read",
            // "https://www.googleapis.com/auth/fitness.blood_pressure.write",
            //"https://www.googleapis.com/auth/fitness.body.read",
            // "https://www.googleapis.com/auth/fitness.body.write",
            // "https://www.googleapis.com/auth/fitness.body_temperature.read",
            // "https://www.googleapis.com/auth/fitness.body_temperature.write",
            // "https://www.googleapis.com/auth/fitness.location.read",
            // "https://www.googleapis.com/auth/fitness.location.write",
            // "https://www.googleapis.com/auth/fitness.nutrition.read",
            // "https://www.googleapis.com/auth/fitness.nutrition.write",
            // "https://www.googleapis.com/auth/fitness.oxygen_saturation.read",
            // "https://www.googleapis.com/auth/fitness.oxygen_saturation.write",
            // "https://www.googleapis.com/auth/fitness.reproductive_health.read",
            // "https://www.googleapis.com/auth/fitness.reproductive_health.write"
            // };

            //Device.OpenUri(new NSUrl(objStringBuider.ToString()));

            ////UIApplication.SharedApplication.OpenUrl(new NSUrl(objStringBuider.ToString()));

             
            //Xamarin.Auth.OAuth2Authenticator objOAuth2Authenticator = new OAuth2Authenticator("abcdefghujkl", "",
            //    "", new Uri("https://consumer.testing.kyor.com/oauth-consent?tracker_type="+type+"&tpa_app"),
            //    new Uri("https://consumer.testing.kyor.com"), new Uri("https://consumer.testing.kyor.com"), null, true);


            Device.OpenUri(new Uri("https://consumer.testing.kyor.com/oauth-consent?tracker_type=" + type + "&tpa_app"));

            // objOAuth2Authenticator.AllowCancel = false;
            // objOAuth2Authenticator.BrowsingCompleted += ObjOAuth2Authenticator_BrowsingCompleted; ; ;
            // objOAuth2Authenticator.Error += ObjOAuth2Authenticator_Error; ; ;
            // objOAuth2Authenticator.ClearCookiesBeforeLogin = true;
            // objOAuth2Authenticator.DoNotEscapeScope = true;
            // objOAuth2Authenticator.ShowErrors = true;
            // objOAuth2Authenticator.Completed += (sender, eventArgs) =>
            // {  
            //     if (eventArgs.IsAuthenticated)
            //     {
            //         App.Current.Properties["access_token"] = eventArgs.Account.Properties["access_token"].ToString();
            //     }
            //     else
            //     {
            //         App.Current.Properties["access_token"] = "";
            //     }

            // };
            // //UIViewController authView = objOAuth2Authenticator.GetUI() as UIViewController;
            // var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            // //topController.PresentViewController((SafariServices.SFSafariViewController)authView, true, null);

            //while (topController.PresentedViewController != null)
            // {
            //  topController = topController.PresentedViewController;
            // }
            //topController.PresentViewController(objOAuth2Authenticator.GetUI(), true, null);
        }

        private void ObjOAuth2Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ObjOAuth2Authenticator_BrowsingCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            
        }
    }
}
