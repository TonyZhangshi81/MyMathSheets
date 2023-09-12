using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHealthController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<HealthCheckResult> GetHealthInfoAsync();
    }
}