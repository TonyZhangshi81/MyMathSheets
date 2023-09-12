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
            var instances = new List<IHealthCheckProvider>();
            foreach (var provider in GetTypesDeriving<IHealthCheckProvider>())
            {
                var instance = (IHealthCheckProvider)Activator.CreateInstance(provider);
                if (instance.IsEnabled)
                {
                    instances.Add(instance);
                }
            }

            await Task.WhenAll(instances.Select(async x => items.Add(await x.GetHealthCheckAsync())));
            stopwatch.Stop();

            result.TimeTakenInSeconds = stopwatch.Elapsed.TotalSeconds;
            result.HealthChecks.AddRange(items.OrderBy(x => x.SortOrder));

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<Type> GetTypesDeriving<T>()
        {
            // TODO 此處需要進一步設置緩存處理機制
            // 遍歷整個程序集，檢索繼承IHealthCheckProvider並且不是抽象類型的對象類型
            return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    from assemblyType in domainAssembly.GetTypes()
                    where typeof(T).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
                    select assemblyType).ToList();
        }
    }
}