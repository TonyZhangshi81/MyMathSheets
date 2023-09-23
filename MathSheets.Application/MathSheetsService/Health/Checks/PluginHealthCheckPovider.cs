using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Plugin;
using MyMathSheets.HealthChecks.Health.Base;
using MyMathSheets.WebHealthChecks.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyMathSheets.WebApi.Health.Checks
{
    /// <summary>
    /// 系統內存狀態檢查
    /// </summary>
    /// <remarks>
    /// Healthy – 文件數於加載數一致
    /// Degraded – 文件數於加載數不一致，表示稍加留意
    /// Unhealthy – 文件數或者加載數量均為0，表示不完整的
    /// </remarks>
    [Export(typeof(HealthControllerBase)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class PluginHealthCheckPovider : HealthControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public PluginHealthCheckPovider() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetHealthCheckConfiguration()
        {
            base.SortOrder = 28;
            base.IsEnabled = true;
        }

        /// <summary>
        /// 檢查結果
        /// </summary>
        public override Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var result = new HealthCheckItemResult(nameof(PluginHealthCheckPovider), base.SortOrder, "題型插件狀態檢查", "檢測情況（文件數與加載數：健康[一致]、留意[不一致]、不健康[文件不存在或未加載]）");
            try
            {
                var metrics = this.GetMetrics();

                var status = HealthState.Healthy;
                if (metrics.PlusinFiles != metrics.PlusinLoad)
                {
                    // 文件數於加載數不一致，表示稍加留意
                    status = HealthState.Degraded;
                }
                if (metrics.PlusinLoad == 0 || metrics.PlusinFiles == 0)
                {
                    // 文件數或者加載數量均為0，表示不完整的
                    status = HealthState.Unhealthy;
                }

                result.HealthState = status;
                result.Messages.Add($"題型插件狀態（總文件數[{metrics.PlusinFiles}]、已加載[{metrics.PlusinLoad}]）.");
            }
            catch
            {
                result.HealthState = HealthState.Unhealthy;
                result.Messages.Add("題型插件狀態檢測中出現異常.");
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual PluginMetrics GetMetrics()
        {
            var fileList = new List<FileInfo>();
            var theFolder = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationUtil.ApplicationRootPath));
            // 遍歷文件
            foreach (var fi in from _ in theFolder.GetFiles(@"MyMathSheets.TheFormulaShows.*.dll")
                               orderby _.Name.Length descending
                               select _)
            {
                fileList.Add(fi);
            }

            PluginMetrics metrics = new PluginMetrics()
            {
                PlusinFiles = fileList.Count,
                PlusinLoad = PluginHelper.GetManager().InitPluginsModuleList.Count
            };
            return metrics;
        }
    }

    /// <summary>
    /// 題型插件狀態
    /// </summary>
    public class PluginMetrics
    {
        /// <summary>
        /// 文件總數
        /// </summary>
        public int PlusinFiles;
        /// <summary>
        /// 已加載
        /// </summary>
        public int PlusinLoad;
    }
}
