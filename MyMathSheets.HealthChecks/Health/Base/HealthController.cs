using MyMathSheets.HealthChecks.Health.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Base
{
    /// <summary>
    /// 
    /// </summary>
    [Export("HealthCheck", typeof(IHealthController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class HealthController : IHealthController
    {
        /// <summary>
        /// 運行狀態檢查
        /// </summary>
        [ImportMany(typeof(HealthControllerBase), RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IEnumerable<Lazy<HealthControllerBase>> HealthCheckProviders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<HealthCheckResult> GetHealthInfoAsync()
        {
            var result = new HealthCheckResult
            {
                MachineName = Environment.MachineName
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var items = new List<HealthCheckItemResult>();
            foreach (var provider in HealthCheckProviders.Where(d => d.Value.IsEnabled))
            {
                items.Add(await provider.Value.GetHealthCheckAsync());
            }
            stopwatch.Stop();

            result.TimeTakenInSeconds = stopwatch.Elapsed.TotalSeconds;
            result.HealthChecks.AddRange(items.OrderBy(x => x.SortOrder));

            return await Task.FromResult(result);
        }
    }
}