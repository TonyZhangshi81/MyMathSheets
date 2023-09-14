using MyMathSheets.HealthChecks.Health.Base;
using MyMathSheets.WebHealthChecks.Base;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MyMathSheets.HealthChecks.Health.Checks
{
    /// <summary>
    /// 系統內存狀態檢查
    /// </summary>
    /// <remarks>
    /// Healthy – up to 80%
    /// Degraded – 80% – 90%
    /// Unhealthy – over 90%
    /// </remarks>
    [Export(typeof(HealthControllerBase)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoryHealthCheckPovider : HealthControllerBase
    {
        private int _percentUsedDegraded = 80;
        private int _percentUsedUnhealthy = 90;

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public MemoryHealthCheckPovider() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetHealthCheckConfiguration()
        {
            base.SortOrder = 29;
            base.IsEnabled = true;

            var percentUsedDegraded = base.OptionSetting.SelectTokens("$.Memory.PercentUsedDegraded");
            if (percentUsedDegraded.Any())
            {
                this._percentUsedDegraded = percentUsedDegraded.ElementAt(0).Value<int>();
            }
            var percentUsedUnhealthy = base.OptionSetting.SelectTokens("$.Memory.PercentUsedUnhealthy");
            if (percentUsedUnhealthy.Any())
            {
                this._percentUsedUnhealthy = percentUsedUnhealthy.ElementAt(0).Value<int>();
            }
        }

        /// <summary>
        /// 檢查結果
        /// </summary>
        public override Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var result = new HealthCheckItemResult(nameof(MemoryHealthCheckPovider), base.SortOrder, "當前環境下的內存狀態檢查", $"檢測內存使用率情況（退化[{_percentUsedDegraded}% – {_percentUsedUnhealthy}%]、不健康[over {_percentUsedUnhealthy}%]）");
            try
            {
                var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var metrics = this.GetMetrics();
                var percentUsed = 100 * metrics.Used / metrics.Total;

                var status = HealthState.Healthy;
                if (percentUsed > _percentUsedDegraded)
                {
                    status = HealthState.Degraded;
                }
                if (percentUsed > _percentUsedUnhealthy)
                {
                    status = HealthState.Unhealthy;
                }

                result.HealthState = status;
                result.Messages.Add($"內存狀態（Total[{metrics.Total}]、Used[{metrics.Used}]、Free[{metrics.Free}]、使用率[ {percentUsed}%]）.");
            }
            catch
            {
                result.HealthState = HealthState.Unhealthy;
                result.Messages.Add("內存檢測中出現異常.");
            }
            return Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual MemoryMetrics GetMetrics()
        {
            MemoryMetrics metrics;

            // 操作系統判定
            if (this.IsUnix())
            {
                // LINUX,OSX
                metrics = GetUnixMetrics();
            }
            else
            {
                // WINDOWS
                metrics = GetWindowsMetrics();
            }

            return metrics;
        }

        /// <summary>
        /// 操作系統判定
        /// </summary>
        /// <returns>windows系統: return false</returns>
        private bool IsUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                         RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            return isUnix;
        }

        /// <summary>
        /// WINDOWS環境下的系統內存狀態
        /// </summary>
        /// <returns></returns>
        private MemoryMetrics GetWindowsMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }

            var lines = output.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var freeMemoryParts = lines[0].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics
            {
                Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0),
                Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0)
            };
            metrics.Used = metrics.Total - metrics.Free;

            return metrics;
        }

        /// <summary>
        /// LINUX,OSX環境下的系統內存狀態
        /// </summary>
        /// <returns></returns>
        private MemoryMetrics GetUnixMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo("free -m")
            {
                FileName = "/bin/bash",
                Arguments = "-c \"free -m\"",
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }

            var lines = output.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var memory = lines[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics
            {
                Total = double.Parse(memory[1]),
                Used = double.Parse(memory[2]),
                Free = double.Parse(memory[3])
            };

            return metrics;
        }
    }

    /// <summary>
    /// 系統內存狀態
    /// </summary>
    public class MemoryMetrics
    {
        /// <summary>
        /// 總量
        /// </summary>
        public double Total;
        /// <summary>
        /// 已使用
        /// </summary>
        public double Used;
        /// <summary>
        /// 未使用
        /// </summary>
        public double Free;
    }
}
