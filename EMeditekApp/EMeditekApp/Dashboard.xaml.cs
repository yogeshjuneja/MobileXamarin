using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EMeditekApp
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();


        }

        private void listReq_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //RequisitionList_Main options = (RequisitionList_Main)e.SelectedItem;            
        }
    }
}
