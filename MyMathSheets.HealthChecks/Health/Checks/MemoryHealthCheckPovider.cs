using MyMathSheets.WebHealthChecks.Base;
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
    [Export(typeof(IHealthCheckProvider)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoryHealthCheckPovider : IHealthCheckProvider
    {
        private const int PercentUsedDegraded = 80;
        private const int PercentUsedUnhealthy = 90;

        /// <summary>
        /// 檢查結果
        /// </summary>
        public Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var result = new HealthCheckItemResult(nameof(MemoryHealthCheckPovider), SortOrder, "當前環境下的內存狀態檢查", "檢測內存使用率情況（退化[80% – 90%]、不健康[over 90%]）");
            try
            {
                var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var metrics = this.GetMetrics();
                var percentUsed = 100 * metrics.Used / metrics.Total;

                var status = HealthState.Healthy;
                if (percentUsed > PercentUsedDegraded)
                {
                    status = HealthState.Degraded;
                }
                if (percentUsed > PercentUsedUnhealthy)
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
                RedirectStandardOutput = true
            };
            info.UseShellExecute = false;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }

            var lines = output.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var freeMemoryParts = lines[0].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics();
            metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
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

            var info = new ProcessStartInfo("free -m");
            info.FileName = "/bin/bash";
            info.Arguments = "-c \"free -m\"";
            info.RedirectStandardOutput = true;

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

        /// <summary>
        /// Defines the order of this provider in the results.
        /// </summary>
        public int SortOrder => 29;

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled => true;
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
