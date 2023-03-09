using Reveal.Sdk;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for Campaigns.xaml
    /// </summary>
    public partial class Campaigns : UserControl
    {
        const string DashboardId = "Campaigns";

        public Campaigns()
        {
            InitializeComponent();
            revealView.InteractiveFilteringEnabled= true;
            revealView.Dashboard = Helpers.GetDashboardFromResources(DashboardId);
            revealView.SaveDashboard += RevealView_SaveDashboard;
        }

        private void RevealView_SaveDashboard(object sender, DashboardSaveEventArgs e)
        {
            //actually not saving, just keeping the changes in memory
            Helpers.DashboardUpdated(DashboardId, e);
            e.SaveFinished();
        }
    }
}
