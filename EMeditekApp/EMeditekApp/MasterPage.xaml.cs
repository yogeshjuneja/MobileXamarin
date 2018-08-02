using EMeditekApp.DashboardModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EMeditekApp
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>();
            
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Insured Details",
                IconSource = "",
                TargetType = typeof(InsuredDetailsNew)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Download E Card",
                IconSource = "",
                TargetType = typeof(DownloadECard)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Claim Status",
                IconSource = "",
                TargetType = typeof(ClaimStatusDetails)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Hospital",
                IconSource = "",
                TargetType = typeof(Hospital)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Claim Information",
                IconSource = "",
                TargetType = typeof(ClaimIntimation)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "E Cashless",
                IconSource = "",
                TargetType = typeof(ECashless)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "E Reimbursement",
                IconSource = "",
                TargetType = typeof(EReimbursement)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Escalation Matrix",
                IconSource = "",
                TargetType = typeof(EscalationMatrix)
            });

            listView.ItemsSource = masterPageItems;
        }
    }
}
