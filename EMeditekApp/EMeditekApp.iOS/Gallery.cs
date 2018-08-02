using EMeditekApp.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ELCImagePicker;
using UIKit;
using Foundation;
 



[assembly: Dependency(typeof(Gallery))]
namespace EMeditekApp.iOS
{
    public class Gallery : IGallery
    {
        private List<byte[]> lstBytes = new List<byte[]>();
        private List<string> _imagename = new List<string>();

        //int? IGallery.ImagesSelection
        //{
        //    get
        //    {
        //        return Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["ImagesSelection"]);
        //    }

        //    set
        //    {
        //        Xamarin.Forms.Application.Current.Properties["ImagesSelection"] = value;
        //    }
        //}

        public void OpenGallery(int imageselection)
        {

            try
            {
                var picker = ELCImagePickerViewController.Create(imageselection, "Select Images");
                int Totalsize = 0;
                bool IsValid = true;
                picker.Completion.ContinueWith(t =>
                {

                    picker.BeginInvokeOnMainThread(() =>
                    {
                        lstBytes = new List<byte[]>();
                        _imagename = new List<string>();
                        picker.DismissViewController(true, null);
                    #region
                    if (t.IsCanceled || t.Exception != null)
                        {
                             
                        //cancelled or error
                    }
                        else
                        {
                        //get the selected items
                        var items = t.Result as List<AssetResult>;
                            foreach (AssetResult aItem in items)
                            {
                                string Extension = aItem.Name.Substring(aItem.Name.IndexOf('.') + 1, aItem.Name.Length - aItem.Name.IndexOf('.') - 1);
                                UIImage objUIImage = aItem.Image;
                                NSData imagedata = new NSData();
                                if (Extension.ToLower() == "png")
                                {
                                    imagedata = objUIImage.AsPNG();
                                }
                                else
                                {
                                    imagedata = objUIImage.AsJPEG();
                                }
                                byte[] bytes = new byte[imagedata.Length];
                                Totalsize += bytes.Length;
                                if (Totalsize > 10000000)
                                {
                                    IsValid = false;
                                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "imagevalidation", "Image Cannot be Greater than 10 MB");
                                    break;
                                }
                                System.Runtime.InteropServices.Marshal.Copy(imagedata.Bytes, bytes, 0, Convert.ToInt32(imagedata.Length));
                                lstBytes.Add(bytes);
                                _imagename.Add(aItem.Name);
                            }


                            if (IsValid)
                            {
                                if (_imagename.Count > 0)
                                {
                                    MessagingCenter.Send<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", lstBytes);
                                    MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedName", _imagename);
                                }
                                else
                                {
                                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "exception", "");
                                }
                            }
                        }



                    #endregion
                });

                });

                var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (topController.PresentedViewController != null)
                {
                    topController = topController.PresentedViewController;
                }
                topController.PresentViewController(picker, true, null);



            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
