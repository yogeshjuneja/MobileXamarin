using Android.Text;
using EMeditekApp.Droid;
using System;
using Xamarin.Forms;
[assembly: Dependency(typeof(ParseHtml))]
namespace EMeditekApp.Droid
{
 public   class ParseHtml : IParseHTML
    {
     public   Object Parse(string HtmlString)
        {
          return   Html.FromHtml(HtmlString);
        }

    }
}