using System;
using Xamarin.Forms;
using EMeditekApp.Droid;
using System.IO;
using Android.Content;

[assembly: Dependency(typeof(SqliteService))]
namespace EMeditekApp.Droid
{
    class SqliteService : ISQLite
    {
        public SqliteService() { }
        #region ISQLite implementation
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "KyorFile.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            Console.WriteLine(path);
            if (!File.Exists(path)) File.Create(path);
            //var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            //var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            //var conn = new SQLite.SQLiteConnection(plat, path);
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection 
            return conn;
        }


        public   string SavePdfFile(byte[] imageByte)
        {

           
            var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments), "KyorReport");
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }
            var file = new Java.IO.File(fileDir, "download.pdf");
            try
            {
                File.WriteAllBytes(file.Path, imageByte);
                file.SetReadable(true);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, "application/pdf");
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
               // File.Delete(file.Path);
            }
            catch (Exception ex)
            {

                return file.Path;
            }
            finally
            {
                
            }
          
            return file.Path;
        }
        #endregion
    }
}