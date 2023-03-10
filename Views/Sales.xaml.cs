using Infragistics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : UserControl
    {
        const string DashboardId = "Sales";
        //public ObservableCollection<RVFilterValue> Territories { get; private set; }

        //private List<object> _selectedTerritories;
        //private DateTime _from;
        //private DateTime _to;
        //private bool _loaded;

        public Sales()
        {
            InitializeComponent();
            this.Loaded += Sales_Loaded;
        }

        private async void Sales_Loaded(object sender, RoutedEventArgs e)
        {
            //if (_loaded)
            //{
            //    return;
            //}
            //_loaded = true;

            //_selectedTerritories = new List<object>();
            //_from = new DateTime(2020, 3, 1);
            //_to = new DateTime(2022, 3, 1);

            revealView.InteractiveFilteringEnabled = true;
            revealView.Dashboard = Helpers.GetDashboardFromResources(DashboardId);
            //revealView.ShowFilters = false;
            //revealView.Dashboard.DateFilter = new RVDateDashboardFilter(RVDateFilterType.CustomRange, new RVDateRange(_from, _to));

            //var territoriesFilter = revealView.Dashboard.Filters.GetByTitle("Territory");
            //var filterValues = await territoriesFilter.GetFilterValuesAsync();
            //Territories = new ObservableCollection<RVFilterValue>(filterValues);
            //this.DataContext = this;

            revealView.DashboardSelectorRequested += RevealView_DashboardSelectorRequested;
            revealView.LinkedDashboardProvider = (dashboardId,asdf) =>
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

        private void Territory_Click(object sender, RoutedEventArgs e)
        {
            //var btn = sender as ToggleButton;
            //var selectedItem = btn?.Tag as RVFilterValue;
            //if (selectedItem == null)
            //{
            //    return;
            //}
            //if (btn.IsChecked.Value)
            //{
            //    _selectedTerritories.Add(selectedItem.Value);
            //} else
            //{
            //    _selectedTerritories.Remove(selectedItem.Value);
            //}
            //var territoryFilter = revealView.Dashboard.Filters.GetByTitle("Territory");
            //territoryFilter.SelectedValues = _selectedTerritories;
        }

        private void DateFromChanged(object sender, RoutedPropertyChangedEventArgs<DateTime> e)
        {
            //if (revealView == null)
            //{
            //    return;
            //}
            //var value = fromDate.SelectedDate.Value;
            //lblFrom.Text = AdjustFromDate(value).ToString("MMM-yyyy");
            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(500);
            //timer.Tick += (s, args) =>
            //{
            //    _from = value;
            //    UpdateDateFilter();
            //    timer.Stop();
            //};
            //timer.Start();
        }

        private void DateToChanged(object sender, RoutedPropertyChangedEventArgs<DateTime> e)
        {
            //if (revealView == null)
            //{
            //    return;
            //}
            //var value = toDate.SelectedDate.Value;
            //lblTo.Text = AdjustToDate(value).ToString("MMM-yyyy");

            //var timer = new DispatcherTimer();
            //timer.Tick += (s, args) =>
            //{
            //    _to = value;
            //    UpdateDateFilter();
            //    timer.Stop();
            //};
            //timer.Start();
        }

        private void UpdateDateFilter()
        {
            //var from = AdjustFromDate(_from);
            //var to = AdjustToDate(_to);
            //var range = new RVDateRange(from, to);
            //var filter = new RVDateDashboardFilter(RVDateFilterType.CustomRange, range);

            //_from = from;
            //_to = to;
            //fromThumb.Value = _from;
            //toThumb.Value = _to;

            //revealView.Dashboard.DateFilter = filter;
        }

        //private DateTime AdjustFromDate(DateTime from)
        //{
        //    //return new DateTime(from.Year, from.Month, 1);
        //}

        //private DateTime AdjustToDate(DateTime to)
        //{
        //    //return new DateTime(to.Year, to.Month, 1).AddMonths(1).AddDays(-1);
        //}

        private void toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (revealView == null)
            //{
            //    return;
            //}
            //var value = toDate.SelectedDate.Value;

            //var timer = new DispatcherTimer();
            //timer.Tick += (s, args) =>
            //{
            //    _to = value;
            //    UpdateDateFilter();
            //    timer.Stop();
            //};
            //timer.Start();
        }

        private void fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (revealView == null)
            //{
            //    return;
            //}
            //var value = fromDate.SelectedDate.Value;

            //var timer = new DispatcherTimer();
            //timer.Tick += (s, args) =>
            //{
            //    _from = value;
            //    UpdateDateFilter();
            //    timer.Stop();
            //};
            //timer.Start();
        }
    }
}
