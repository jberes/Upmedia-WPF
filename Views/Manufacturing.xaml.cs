using Reveal.Sdk;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for Manufacturing.xaml
    /// </summary>
    public partial class Manufacturing : UserControl
    {
        const string DashboardId = "Manufacturing";

        public Manufacturing()
        {
            InitializeComponent();
            revealView.InteractiveFilteringEnabled = true;
            revealView.Dashboard = Helpers.GetDashboardFromResources(DashboardId);
            DataContext = revealView;
            revealView.SaveDashboard += RevealView_SaveDashboard;
        }

        private void RevealView_SaveDashboard(object sender, DashboardSaveEventArgs e)
        {
            //actually not saving, just keeping the changes in memory
            Helpers.DashboardUpdated(DashboardId, e);
            e.SaveFinished();
        }

        private void Button_Maximize_Viz_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as ToggleButton;
            var vizId = btn?.Tag as string;
            if (vizId == null)
            {
                return;
            }
            ClearVisualizationButtons(btn);
            var maximizedViz = revealView.MaximizedVisualization;
            if (maximizedViz == null || maximizedViz.Id != vizId)
            {
                revealView.MaximizeVisualization(revealView.Dashboard.Visualizations.GetById(vizId));
            } else if (maximizedViz != null)
            {
                revealView.MinimizeVisualization();
            }
        }

        private void ClearVisualizationButtons(ToggleButton btn)
        {
            foreach (var item in itmVisualizations.Items)
            {
                var itmPresenter = (ContentPresenter)itmVisualizations.ItemContainerGenerator.ContainerFromItem(item);
                var itmButton = itmPresenter.ContentTemplate.FindName("vizToggleButton", itmPresenter) as ToggleButton;
                if (itmButton != null && itmButton != btn)
                {
                    itmButton.IsChecked = false;
                }
            }
        }
    }
}
