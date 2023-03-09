using Reveal.Sdk;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for Marketing.xaml
    /// </summary>
    public partial class Marketing : UserControl
    {
        const string DashboardId = "Marketing";

        public Marketing()
        {
            InitializeComponent();
            revealView.InteractiveFilteringEnabled = true;
            revealView.Dashboard = Helpers.GetDashboardFromResources(DashboardId);
            revealView.DashboardSelectorRequested += RevealView_DashboardSelectorRequested;
            revealView.LinkedDashboardProvider = (dashboardId, linkTitle) =>
            {
                return Helpers.GetDashboardFromResources(dashboardId);
            };
            revealView.SaveDashboard += RevealView_SaveDashboard;
        }

        private void RevealView_SaveDashboard(object sender, DashboardSaveEventArgs e)
        {
            //actually not saving, just keeping the changes in memory
            Helpers.DashboardUpdated(DashboardId, e);
            e.SaveFinished();
        }

        private void RevealView_DashboardSelectorRequested(object sender, DashboardSelectorRequestedEventArgs e)
        {
            var w = new OpenDashboardWindow();
            Helpers.AddSampleDashboards(w.Dashboards);
            w.LocateRelativeTo(revealView, e.RelativePoint);
            w.ShowDialog();
            e.Callback(w.SelectedDashboardReference?.Id);
        }
    }
}
