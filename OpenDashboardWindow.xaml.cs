using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for OpenDashboardWindow.xaml
    /// </summary>
    public partial class OpenDashboardWindow : Window
    {
        public ObservableCollection<RVDashboardReference> Dashboards { get; set; }
        public event EventHandler<RVDashboardReference> SelectedDashboard;
        public RVDashboardReference SelectedDashboardReference { get; private set; }
        public OpenDashboardWindow()
        {
            InitializeComponent();
            DataContext = this;

            Dashboards = new ObservableCollection<RVDashboardReference>();
            WindowStyle = WindowStyle.ToolWindow;
        }

        public void LocateRelativeTo(Visual view, Point relativePosition)
        {
            var screenPoint = PresentationSource.FromVisual(view).CompositionTarget.TransformFromDevice.Transform(view.PointToScreen(relativePosition));
            this.Left = screenPoint.X - this.Width;
            this.Top = screenPoint.Y - this.Height / 2;
        }

        private void DashboardSelected_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dashboard = Dashboards.First(d => d.Id.Equals(btn?.Tag));
            if (dashboard != null)
            {
                SelectedDashboard?.Invoke(this, dashboard);
            }
            SelectedDashboardReference = dashboard;
            Hide();
        }
    }

    public class RVDashboardReference
    {
        public string Id { get; private set; }
        public RVDashboard Dashboard { get; private set; }

        public RVDashboardReference(string id, RVDashboard dashboard)
        {
            this.Id = id;
            this.Dashboard = dashboard;
        }

        public ImageSource Image
        {
            get
            {
                return Dashboard.GetThumbnailImage(250, 200);
            }
        }
    }
}
