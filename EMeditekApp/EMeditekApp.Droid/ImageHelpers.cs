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
using Android.Graphics;
using Android.Media;
using System.IO;
using Xamarin.Forms;
using EMeditekApp.Droid;

[assembly: Dependency(typeof(ImageHelpers))]
namespace EMeditekApp.Droid
{
    class ImageHelpers : IImageHelpers,IImageCompress
    {
        public   string SaveFile(string collectionName, byte[] imageByte, string fileName)
        {
            var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }
            var file = new Java.IO.File(fileDir, fileName);
            // WriteAllBytes(file.Path, imageByte);
            return file.Path;
        }
        public   byte[] RotateImage(string path)
        {
            byte[] imageBytes;
            var originalImage = BitmapFactory.DecodeFile(path);
          
            var rotation = GetRotation(path);
            var width = (originalImage.Width/2);
            var height = (originalImage.Height/2);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);
            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }
            using (var ms = new MemoryStream())
            {
                rotatedImage.Compress(Bitmap.CompressFormat.Jpeg, 50, ms);
                imageBytes = ms.ToArray();
            }
            try
            {
                originalImage.Recycle();
                rotatedImage.Recycle();
                originalImage.Dispose();
                rotatedImage.Dispose();
                GC.Collect();
            }
            catch (Exception)
            {
                return imageBytes;

            }
            return imageBytes;
        }
        public   int GetRotation(string filePath)
        {
            var ei = new ExifInterface(filePath);
            var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);
            switch (orientation)
            {
                case Android.Media.Orientation.Rotate90:
                    return 90;
                case Android.Media.Orientation.Rotate180:
                    return 180;
                case Android.Media.Orientation.Rotate270:
                    return 270;
                default:
                    return 0;
            }
        }
        public   string SavePdfFile(string collectionName, byte[] imageByte, string fileName)
        {

            var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments), collectionName);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }
            var file = new Java.IO.File(fileDir, fileName);
            // WriteAllBytes(file.Path, imageByte);
            return file.Path;
        }
        public   byte[] ResizeImage(byte[] imageData, float? width, float? height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            if (width == null)
            {
                width = (int)originalImage.Width;
            }
            if (height == null)
            {
                height = (int)originalImage.Height;
            }
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 70, ms);
                return ms.ToArray();
            }
        }

        
    }
}