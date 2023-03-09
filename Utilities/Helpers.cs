using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    internal static class Helpers
    {
        private static Dictionary<string, byte[]> modifiedDashboards = new Dictionary<string, byte[]>();

        public static Stream GetDashboardStreamFromFile(string dashboardId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Dashboards", $"{dashboardId}.rdash");

            return File.Exists(path) ? File.OpenRead(path) : null;
        }

        public static RVDashboard GetDashboardFromResources(string dashboardId)
        {
            Stream stream;
            byte[] contents;
            if (modifiedDashboards.TryGetValue(dashboardId, out contents))
            {
                stream = new MemoryStream(contents);
            } else
            {
                stream = GetDashboardStreamFromFile(dashboardId);
            }
            if (stream == null) return null;
            using (stream)
            {
                return new RVDashboard(stream);
            }
        }
        public static RVDashboardReference GetDashboardReference(string id)
        {
            return new RVDashboardReference(id, GetDashboardFromResources(id));
        }

        public static void AddSampleDashboards(IList<RVDashboardReference> references)
        {
            references.Add(GetDashboardReference("Sales"));
            references.Add(GetDashboardReference("Marketing"));
            references.Add(GetDashboardReference("Manufacturing"));
            references.Add(GetDashboardReference("Campaigns"));
        }

        public static async void DashboardUpdated(string dashboardId, DashboardSaveEventArgs e)
        {         
            var path = Path.Combine(Environment.CurrentDirectory, "Dashboards", $"{e.Name}.rdash");
            
            if (File.Exists(path))
            {
                var data = await e.Serialize();
                using (var output = File.Open(path, FileMode.Open))
                {
                    output.Write(data, 0, data.Length);
                }
            }
            else
            {
                //var data = await e.SerializeWithNewName(Path.Combine(Environment.CurrentDirectory, "Dashboards", $"{e.Name}"));
                var dashboardName = Path.Combine(Environment.CurrentDirectory, "Dashboards", $"{e.Name}");
                using (var stream = new FileStream(dashboardName, FileMode.Create, FileAccess.Write))
                {
                    var name = Path.GetFileNameWithoutExtension(dashboardName);
                    var data = await e.SerializeWithNewName(name);
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
            // This commented code block would ignore the Save
            //var path = Path.Combine(_defaultDirectory, $"{e.Name}.rdash");
            //e.Serialize().ContinueWith((t) =>
            //{
            //    modifiedDashboards[dashboardId] = t.Result;
            //});
        }

        internal static string GetLocalFilesFolder()
        {
            var loc = Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetDirectoryName(loc);

            return Path.Combine(dir, "DataSources");
        }
    }
}
