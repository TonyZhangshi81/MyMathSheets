using MyMathSheets.WebHealthChecks.Base;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Checks
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IHealthCheckProvider)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class DiskSpaceHealthCheckProvider : IHealthCheckProvider
    {
        private const int MinPercentageFree = 10;
        private const int WarningPercentageFree = 20;

        /// <summary>
        /// Returns the health heck info.
        /// </summary>
        public Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var result = new HealthCheckItemResult(nameof(DiskSpaceHealthCheckProvider), SortOrder, "當前部署環境下磁盤空間檢查", $"验证可用磁盘空间是否大于 {MinPercentageFree}%.");
            try
            {
                var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var percentageFree = GetPercentageFree(drive);
                result.HealthState = DetermineState(percentageFree);
                result.Messages.Add($"可用磁盤空間{(percentageFree > MinPercentageFree ? "大於" : "小於")} {MinPercentageFree}%.");
            }
            catch
            {
                result.HealthState = HealthState.Unhealthy;
                result.Messages.Add("當前磁盤不存在.");
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        private static double GetPercentageFree(string drive)
        {
            var driveInfo = new DriveInfo(drive);
            var freeSpace = driveInfo.TotalFreeSpace;
            var totalSpace = driveInfo.TotalSize;
            var percentageFree = Convert.ToDouble(freeSpace) / Convert.ToDouble(totalSpace) * 100;
            return percentageFree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentageFree"></param>
        /// <returns></returns>
        private static HealthState DetermineState(double percentageFree)
        {
            if (percentageFree < MinPercentageFree)
            {
                return HealthState.Unhealthy;
            }
            if (percentageFree < WarningPercentageFree)
            {
                return HealthState.Degraded;
            }
            return HealthState.Healthy;
        }

        /// <summary>
        /// Defines the order of this provider in the results.
        /// </summary>
        public int SortOrder => 30;

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled => true;
    }
}