using MyMathSheets.WebHealthChecks.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyMathSheets.HealthChecks.Health.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HealthControllerBase : IHealthCheckProvider, IConfigureHealthCheck
    {
        /// <summary>
        /// 定義各項檢查的執行順序（同時也是檢查報告的信息顯示順序）
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 設定是否有效使用的開關
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JObject OptionSetting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HealthControllerBase()
        {
            this.OptionInit();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OptionInit()
        {
            this.SortOrder = 30;
            this.IsEnabled = true;

            var optionJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "health.option.json");
            if (!File.Exists(optionJsonPath))
            {
                return;
            }

            var file = File.OpenText(optionJsonPath);
            var reader = new JsonTextReader(file);
            this.OptionSetting = (JObject)JToken.ReadFrom(reader);

            this.SetHealthCheckConfiguration();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract void SetHealthCheckConfiguration();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Task<HealthCheckItemResult> GetHealthCheckAsync();

    }
}
