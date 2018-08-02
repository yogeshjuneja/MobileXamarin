using Android.App;
using Android.Content;
using Android.Support.CustomTabs;
using EMeditekApp.Droid;
using EMeditekApp.Wellogo;
using Plugin.CurrentActivity;
using System;
using System.Text;
 using Xamarin.Auth;
using Xamarin.Forms.OAuth;
using Xamarin.Forms;
using Xamarin.Auth.Extensions;



[assembly: Dependency(typeof(OAuth))]
namespace EMeditekApp.Droid
{
    class OAuth : IOAuth
    {
      
        public async void GetAuthoriseToken( int type)
        {
            #region Comented
            StringBuilder objStringBuider = new StringBuilder();
              objStringBuider.Append("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=299559447421-56url4318ir5l1h4lcv9uhklnsqcfaim.apps.googleusercontent.com&redirect_uri=http://www.emeditek.co.in/download/close.html");
              objStringBuider.Append("&scope=https://www.googleapis.com/auth/fitness.activity.read");
           // objStringBuider.Append(" https://www.googleapis.com/auth/fitness.activity.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.activity.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_glucose.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_glucose.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_pressure.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.blood_pressure.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body_temperature.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.body_temperature.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.location.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.location.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.nutrition.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.nutrition.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.oxygen_saturation.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.oxygen_saturation.write");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.reproductive_health.read");
            objStringBuider.Append(" https://www.googleapis.com/auth/fitness.reproductive_health.write");
            #endregion
            //OAuth2Authenticator objAuthenticator = new OAuth2Authenticator(
            //      clientId: "abcdefghujkl",
            //scope: "",
            //authorizeUrl: new Uri("https://consumer.testing.kyor.com/oauth-consent?tracker_type=2&tpa_app"),
            //redirectUrl: new Uri("https://consumer.testing.kyor.com"),
            //isUsingNativeUI: true
            //    );
            //objAuthenticator.AccessTokenUrl= new Uri("https://consumer.testing.kyor.com");
            //string URL = "https://consumer.testing.kyor.com/oauth-consent?tracker_type=2&tpa_app";
            // Intent myIntent = new Intent(Intent.ActionView,Android.Net.Uri.Parse(URL.ToString()));
            //  ((Activity)Forms.Context).StartActivityForResult(myIntent,2);
            //WebView browser = new WebView();
            //browser.Source = "https://consumer.testing.kyor.com/oauth-consent?tracker_type=2";
            //browser.Navigated += Browser_Navigated;
            


 //var activity = CrossCurrentActivity.Current.Activity as Activity;
            //Xamarin.Auth.OAuth2Authenticator objOAuth2Authenticator = new Xamarin.Auth.OAuth2Authenticator("299559447421-56url4318ir5l1h4lcv9uhklnsqcfaim.apps.googleusercontent.com", "code",
            // objStringBuider.ToString(), new Uri("https://accounts.google.com/o/oauth2/auth"),
            //     new Uri("http://www.emeditek.co.in/download/close.html"), new Uri("http://www.emeditek.co.in"), null, true);


            //OAuthProviderDefinition objOAuthProviderDefinition = new OAuthProviderDefinition(null, "https://squareboat.kyor.com/oauth-consent?tracker_type=2", null, null, null, null, null, null);
            // OAuthProvider objOAuthProvider=      Xamarin.Forms.OAuth.OAuthProviders.Custom(objOAuthProviderDefinition);
            // var a =  await Xamarin.Forms.OAuth.OAuthAuthenticator.Authenticate(objOAuthProvider);

            // if(a!=null)
            // {

            // }
            // OAuthProvider objOAuthProviderDefinition=OAuthProviders.Google("299559447421-56url4318ir5l1h4lcv9uhklnsqcfaim.apps.googleusercontent.com", "http://www.emeditek.co.in/download/close.html", new string[] { "https://www.googleapis.com/auth/fitness.activity.read" });
            //var a=    Xamarin.Forms.OAuth.OAuthAuthenticator.Authenticate(objOAuthProviderDefinition);
            #region Xamarin.Auth
            var activity = CrossCurrentActivity.Current.Activity as Activity;
            Xamarin.Auth.OAuth2Authenticator objOAuth2Authenticator = new OAuth2Authenticator("abcdefghujkl", "",
                "", new Uri("https://consumer.testing.kyor.com/oauth-consent?tracker_type="+ type + "&tpa_app"),
                new Uri("https://consumer.testing.kyor.com"), new Uri("https://consumer.testing.kyor.com"), null, true);

            
            objOAuth2Authenticator.AllowCancel = false;
            objOAuth2Authenticator.BrowsingCompleted += ObjOAuth2Authenticator_BrowsingCompleted;
            objOAuth2Authenticator.Error += ObjOAuth2Authenticator_Error;
            objOAuth2Authenticator.ClearCookiesBeforeLogin = true;
            objOAuth2Authenticator.DoNotEscapeScope = true;
            objOAuth2Authenticator.ShowErrors = true;
            objOAuth2Authenticator.PlatformUIMethod += ObjOAuth2Authenticator_PlatformUIMethod;
            objOAuth2Authenticator.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    App.Current.Properties["access_token"] = eventArgs.Account.Properties["access_token"].ToString();
                }
                else
                {
                    App.Current.Properties["access_token"] = "";
                }
            };

            //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            //presenter.Login(objOAuth2Authenticator);
            //presenter.Completed += Presenter_Completed;
           // activity.StartActivity(objOAuth2Authenticator.GetUI(activity));
           //var authActivity = new CustomTabsIntent.Builder().Build();
           //authActivity.Intent = objOAuth2Authenticator.GetUI(activity) as Intent;
           // authActivity.LaunchUrl(activity, Android.Net.Uri.Parse(objStringBuider.ToString()));
           //   ((Activity)Forms.Context).StartActivityForResult(authActivity.Intent, 2);
            Forms.Context.StartActivity(objOAuth2Authenticator.GetUI(activity));
            #endregion
           
        }

        private void Presenter_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private Intent ObjOAuth2Authenticator_PlatformUIMethod(Context context)
        {
            throw new NotImplementedException();
        }

        private void ObjOAuth2Authenticator_Error(object sender,  Xamarin.Auth.AuthenticatorErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ObjOAuth2Authenticator_BrowsingCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}