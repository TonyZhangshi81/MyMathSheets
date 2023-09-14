using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Base
{
    /// <summary>
    /// 定義運行狀況檢查的接口
    /// </summary>
    public interface IHealthCheckProvider
    {
        /// <summary>
        /// 返回檢查報告
        /// </summary>
        Task<HealthCheckItemResult> GetHealthCheckAsync();
    }
}