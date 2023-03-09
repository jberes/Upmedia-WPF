using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            _tabControl.SelectedIndex = _tabControl.Items.Count - 1;
            var tab = (TabItem)_tabControl.Items[_tabControl.Items.Count - 1];
            var createDashboardView = (EmptyDashboard)tab.Content;
            createDashboardView.NewDashboard();
        }
        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            var w = new OpenDashboardWindow();
            Helpers.AddSampleDashboards(w.Dashboards);
            w.SelectedDashboard += SelectedDashboard;
            w.ShowDialog();
        }

        private void SelectedDashboard(object sender, RVDashboardReference reference)
        {
            _tabControl.SelectedIndex = _tabControl.Items.Count - 1;
            var tab = (TabItem)_tabControl.Items[_tabControl.Items.Count - 1];
            var createDashboardView = (EmptyDashboard)tab.Content;
            createDashboardView.OpenDashboard(reference.Dashboard);
        }        

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
