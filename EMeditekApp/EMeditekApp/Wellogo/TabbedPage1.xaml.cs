using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
   
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            
            InitializeComponent();
            BindMainMenu();
        }

        void BindMainMenu()
        {
            
            //List<WellogoMainMenu> lstWellogoMainMenu = new List<WellogoMainMenu>();
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Executive Health Checkups"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Home Health"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Health Risk Assesment"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Order Medicine"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Ambulance Services"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Blood Banks"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Talk To Doctor"));
            //lstWellogoMainMenu.Add(new WellogoMainMenu("Fitness Tracker"));
            //lstWellogo.ItemsSource = lstWellogoMainMenu;
        }
    }


    //public class WellogoMainMenu
    //{
    //    public string Name { get; set; }

    //    public WellogoMainMenu(string _Name)
    //    {
    //        Name = _Name;
    //    }
    //}
}