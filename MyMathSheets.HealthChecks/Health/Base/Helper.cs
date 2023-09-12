using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        public IHealthController _healthController
        {
            get;
            set;
        }

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
            return await this._healthController.GetHealthInfoAsync();
        }
    }
}