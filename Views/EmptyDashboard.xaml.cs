using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for EmptyDashboard.xaml
    /// </summary>
    public partial class EmptyDashboard : UserControl
    {
        const string DashboardId = "NewDashboard";

        public EmptyDashboard()
        {
            InitializeComponent();

            revealView.Dashboard = Helpers.GetDashboardFromResources(DashboardId) ?? new RVDashboard();
            revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
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

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            var datasources = new List<RVDashboardDataSource>();
            var datasourceItems = new List<RVDataSourceItem>();

            //var northWindCustomersDatasource = new RVWebResourceDataSource();
            //northWindCustomersDatasource.Id = "northwindCustomersDataSource";
            //northWindCustomersDatasource.UseAnonymousAuthentication = true;
            //northWindCustomersDatasource.Url = "http://northwind.servicestack.net/Customers.csv";
            //northWindCustomersDatasource.Title = "Northwind Customers";
            //datasources.Add(northWindCustomersDatasource);

            var northWindOrdersDatasource = new RVWebResourceDataSource();
            northWindOrdersDatasource.Id = "northwindOrdersDataSource";
            northWindOrdersDatasource.UseAnonymousAuthentication = true;
            //northWindOrdersDatasource.Url = "http://northwind.servicestack.net/query/orders.json";

            northWindOrdersDatasource.Url = "https://northwindcloud.azurewebsites.net/api/orders";

            northWindOrdersDatasource.Title = "Northwind Orders";
            datasources.Add(northWindOrdersDatasource);

            var localDatasource = new RVLocalFileDataSourceItem();
            localDatasource.Uri = "local:/Samples.xlsx";
            localDatasource.Title = "Samples.xlsx";
            RVExcelDataSourceItem excelDatasourceItem = new RVExcelDataSourceItem(localDatasource);
            excelDatasourceItem.Title = "Sample Data";
            datasourceItems.Add(excelDatasourceItem);

            e.Callback(new RevealDataSources(datasources, datasourceItems, true));
        }

        internal void NewDashboard()
        {
            revealView.Dashboard = new RVDashboard();
        }

        internal void OpenDashboard(RVDashboard dashboard)
        {
            revealView.Dashboard = dashboard;
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
