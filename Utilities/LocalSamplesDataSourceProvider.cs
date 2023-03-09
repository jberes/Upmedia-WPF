using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reveal.Sdk.Samples.UpMedia.Wpf
{
    internal class LocalSamplesDataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDataSourceItemAsync(RVDataSourceItem dataSourceItem)
        {
            if (IsSamplesWebResource(dataSourceItem))
            {
                return Task.FromResult(CreateLocalSamplesDataSourceItem((RVExcelDataSourceItem)dataSourceItem));
            }
            else
            {
                return Task.FromResult<RVDataSourceItem>(null);
            }
        }

        private static bool IsSamplesWebResource(RVDataSourceItem dataSourceItem)
        {
            var excelItem = dataSourceItem as RVExcelDataSourceItem;
            var wrItem = excelItem?.ResourceItem as RVWebResourceDataSourceItem;
            return wrItem != null && wrItem.Url.EndsWith("Samples.xlsx");
        }

        private static RVDataSourceItem CreateLocalSamplesDataSourceItem(RVExcelDataSourceItem excelItem)
        {
            var localItem = new RVLocalFileDataSourceItem();
            localItem.Uri = "local:/Samples.xlsx";
            excelItem.ResourceItem = localItem;
            return excelItem;
        }
    }
}
