using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyMathSheets.WebApi.Health.Base
{
    /// <summary>
    /// 
    /// </summary>
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
                instances.Add(instance);
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
            return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    from assemblyType in domainAssembly.GetTypes()
                    where typeof(T).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
                    select assemblyType).ToList();
        }
    }
}