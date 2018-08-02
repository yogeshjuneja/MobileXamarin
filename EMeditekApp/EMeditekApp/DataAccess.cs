using EMeditekApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace EMeditekApp
{
    public class DataAccess
    {
        static object locker = new object();
        SQLiteConnection dbConn;
        public DataAccess()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
            // create the table(s)
            dbConn.CreateTable<LoggedInUser>();
            dbConn.CreateTable<UserMenu>();
        }
        public LoggedInUser IsUserLogedIn()
         {
            lock (locker)
            {
                return dbConn.Table<LoggedInUser>().FirstOrDefault();
            }
        }
        public List<UserMenu> HasMenu()
        {
            lock (locker)
            {
                return dbConn.Table<UserMenu>().ToList();
            }
        }

        public int SaveItem(LoggedInUser item)
        {
            lock (locker)
            {
                 
                if (IsUserLogedIn() != null)
                {
                    dbConn.Update(item);
                    return item.id;
                }
                else
                {
                    return dbConn.Insert(item);
                }
            }
        }

        public int SaveUserMenu(List<UserMenu> lstuserMenu)
        {
            lock (locker)
            {
                if (HasMenu() != null)
                {
                    dbConn.Execute("delete from Usermenu");
                }
                foreach (UserMenu u in lstuserMenu)
                {
                    dbConn.Insert(u);
                }
                return lstuserMenu.Count;
            }
        }
        public int Logout()
        {
            lock (locker)
            {
                return dbConn.Delete<LoggedInUser>(App.id);
            }
        }

        
        
    }
}