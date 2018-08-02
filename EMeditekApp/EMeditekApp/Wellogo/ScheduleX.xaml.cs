using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleX : ContentPage
    {
        List<string> lstDrugs = new List<string>() { "Amobarbital","Amphetamine","Amphetamine","Barbital","Cyclobarbital","Dexamphetamine","Ethclorvynol"
        ,"Glutethimide","Meprobamate","Methamphetamine","Methaqualone","Methylphenidate","Methylphenobarbital","Oseltamivir","Pentobarbital"
        ,"Phencyclidine","Phenometrazine","Rimantadine","Secobarbital","Zanamivir"};
        
        public ScheduleX()
        {
            InitializeComponent();
            this.Title = "List of Schedule X Medicines";
            lvdrugs.ItemsSource = lstDrugs;
        }
        private void close_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PopModalAsync(true);
        }

        private void lstDrugs_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}