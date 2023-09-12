using System.Threading.Tasks;

namespace MyMathSheets.WebApi.Health.Base
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