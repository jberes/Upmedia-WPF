using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            RevealSdkSettings.EnableNewCharts= true;
            RevealSdkSettings.LocalDataFilesRootFolder = Helpers.GetLocalFilesFolder();
            RevealSdkSettings.Caching.CachePath = @"C:\Temp\Reveal\Wpf\Cache";
            RevealSdkSettings.Caching.DataCachePath = @"C:\Temp\Reveal\Wpf\Cache";
            RevealSdkSettings.DataSourceProvider = new LocalSamplesDataSourceProvider();
        }
    }
}
