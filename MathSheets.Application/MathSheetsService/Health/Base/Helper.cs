using System.Threading.Tasks;

namespace MyMathSheets.WebApi.Health.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        IHealthController _healthController { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Helper()
        {
            this._healthController = new HealthController();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HealthCheckResult> GetHealthInfoAsync()
        {
            return await _healthController.GetHealthInfoAsync();
        }
    }
}