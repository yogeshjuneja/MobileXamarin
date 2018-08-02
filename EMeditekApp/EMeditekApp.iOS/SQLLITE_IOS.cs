using EMeditekApp.iOS;
using Foundation;
using QuickLook;
using SQLite;
using System;
using System.IO;
using UIKit;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLLITE_IOS))]
namespace EMeditekApp.iOS
{
    class SQLLITE_IOS: ISQLite
    {
        public SQLLITE_IOS()
        {

        }

        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "EmeditekDBFile.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            //var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            //var conn = new SQLite.Net.SQLiteConnection(plat, path);
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection 
            return conn;
            //var sqliteFilename = "EmeditekDBFile.db3";
            //string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            //string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            //var path = Path.Combine(libraryPath, sqliteFilename);

            //// This is where we copy in the prepopulated database
            //Console.WriteLine(path);
            //if (!File.Exists(path))
            //{
            //    File.Copy(sqliteFilename, path);
            //}

            //var conn = new SQLite.SQLiteConnection(path);

            //// Return the database connection 
            //return conn;
        }


        public string SavePdfFile(byte[] imageByte)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documentsPath, "download.pdf");
            File.WriteAllBytes(filePath, imageByte);


            FileInfo fi = new FileInfo(filePath);

            QLPreviewController previewController = new QLPreviewController();
            previewController.DataSource = new PDFPreviewControllerDataSource(fi.FullName, fi.Name);

            UINavigationController controller = FindNavigationController();
            if (controller != null)
                controller.PresentViewController(previewController, true, null);
            
            return filePath;
        }


        private UINavigationController FindNavigationController()
        {
            foreach (var window in UIApplication.SharedApplication.Windows)
            {
                if (window.RootViewController.NavigationController != null)
                    return window.RootViewController.NavigationController;
                else
                {
                    UINavigationController val = CheckSubs(window.RootViewController.ChildViewControllers);
                    if (val != null)
                        return val;
                }
            }

            return null;
        }

        private UINavigationController CheckSubs(UIViewController[] controllers)
        {
            foreach (var controller in controllers)
            {
                if (controller.NavigationController != null)
                    return controller.NavigationController;
                else
                {
                    UINavigationController val = CheckSubs(controller.ChildViewControllers);
                    if (val != null)
                        return val;
                }
            }
            return null;
        }
        #endregion
    }

    public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
    {
        string url = "";
        string filename = "";

        public PDFPreviewControllerDataSource(string url, string filename)
        {
            this.url = url;
            this.filename = filename;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return new PDFItem(filename, url);
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }


    }


    public class PDFItem : QLPreviewItem
    {
        string title;
        string uri;

        public PDFItem(string title, string uri)
        {
            this.title = title;
            this.uri = uri;
        }

        public override string ItemTitle
        {
            get { return title; }
        }

        public override NSUrl ItemUrl
        {
            get { return NSUrl.FromFilename(uri); }
        }
    }

}