using System.Threading.Tasks;

namespace MyMathSheets.WebApi.Health.Base
{
    /// <summary>
    /// Defines the interface for a health check provider.
    /// </summary>
    public interface IHealthCheckProvider
    {
        /// <summary>
        /// Returns the health heck info.
        /// </summary>
        Task<HealthCheckItemResult> GetHealthCheckAsync();

        /// <summary>
        /// Defines the order of this provider in the results.
        /// </summary>
        int SortOrder { get; }
    }
}