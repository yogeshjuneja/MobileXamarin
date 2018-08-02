using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace EMeditekApp.Droid
{
    [Activity(Label = "OAuth2Activity")]
    [IntentFilter(
    actions: new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataScheme = OAuthManager.REDIRECT_SCHEME,
    DataPath = OAuthManager.REDIRECT_PATH
)]
    public class OAuth2Activity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Uri uri = new Uri(Intent.Data.ToString());
           OAuthManager.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}