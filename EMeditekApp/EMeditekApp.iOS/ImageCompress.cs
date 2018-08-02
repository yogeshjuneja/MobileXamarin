using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UIKit;

namespace EMeditekApp.iOS
{
    class ImageCompress :IImageCompress
    { 

        public byte[] ResizeImage(byte[] imageData, float? width , float? height )
        {
            UIImage originalImage = ImageFromByteArray(imageData);
          

            if (width == null)
            {
                width = (int)originalImage.Size.Width;
            }
            if (height == null)
            {
                height = (int)originalImage.Size.Height;
            }
            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, (float)width, (float)height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
