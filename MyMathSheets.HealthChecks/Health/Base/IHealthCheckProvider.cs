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

        /// <summary>
        /// 定義各項檢查的執行順序（同時也是檢查報告的信息顯示順序）
        /// </summary>
        int SortOrder { get; }

        /// <summary>
        /// 設定是否有效使用的開關
        /// </summary>
        bool IsEnabled { get; }
    }
}