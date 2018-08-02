using System;
using System.Collections.Generic;
using System.Text;
using EMeditekApp.iOS;
using Xamarin.Forms;
using Foundation;

[assembly: Dependency(typeof(ParseHtml))]
namespace EMeditekApp.iOS
{
    class ParseHtml :IParseHTML
    {
        public Object Parse(string HtmlString)
        {
            NSError error = null;

            NSAttributedString attributedString = new NSAttributedString(HtmlString,
                new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8 },
                ref error);

            return attributedString.Value;
        }
    }
}
