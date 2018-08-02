using EMeditekApp.Droid;
using Firebase;
using Firebase.Iid;
using Xamarin.Forms;
using Xamarin.Forms.OAuth;


[assembly: Dependency(typeof(Token))]
namespace EMeditekApp.Droid
{
    public class Token : IToken
    {
        public Token()
        {


        }

        public string GetToken()
        {
 

            //var FirebaseOptions = new FirebaseOptions.Builder()
            //                                       .SetApplicationId("1:121145201934:android:fcdaf2aed1a3c476")
            //                                       .SetApiKey("AIzaSyC9sXrpUINfNThyAXI2-WuSjA44IjBmp3A")
            //                                       .SetGcmSenderId("121145201934")
            //                                       .SetDatabaseUrl("https://emeditekapp.firebaseio.com/")//com.EMeditekApp.xamarin
            //                                       .SetStorageBucket("gs://emeditekapp.appspot.com/")
            //                                       .Build();

            //FirebaseApp firebaseApp = FirebaseApp.InitializeApp(Android.App.Application.Context, FirebaseOptions);




            //return "ABC";
            return FirebaseInstanceId.Instance.Token;
        }
    }
}