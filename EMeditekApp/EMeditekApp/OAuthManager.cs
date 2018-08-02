using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace EMeditekApp
{
    public static class OAuthManager
    {
        private const string CLIENT_ID = "abcdefghujkl";
        private const string AUTH_SCOPE = "";               // Or whatever you want
        private const string AUTH_URI = "https://consumer.testing.kyor.com/oauth-consent?tracker_type=2&tpa_app";
        private const string ACCESSTOKEN_URI = "https://consumer.testing.kyor.com";

        public const string REDIRECT_SCHEME = "com.EMeditekApp.xamarin";    // e.g. com.example.myapp
        public const string REDIRECT_PATH = "auth";                 // You can change this too

        public static readonly OAuth2Authenticator Authenticator;

        static OAuthManager()
        {
            Authenticator = new OAuth2Authenticator(
            clientId: CLIENT_ID,
            scope: AUTH_SCOPE,
            authorizeUrl: new Uri(AUTH_URI),
            redirectUrl: new Uri(REDIRECT_SCHEME + ":/" + REDIRECT_PATH),
            isUsingNativeUI: true
        );

            Authenticator.AccessTokenUrl = new Uri(ACCESSTOKEN_URI);
        }
    }
}
