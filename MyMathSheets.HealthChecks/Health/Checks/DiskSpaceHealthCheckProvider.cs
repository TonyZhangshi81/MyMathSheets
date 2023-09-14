using MyMathSheets.HealthChecks.Health.Base;
using MyMathSheets.WebHealthChecks.Base;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyMathSheets.WebHealthChecks.Checks
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(HealthControllerBase)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class DiskSpaceHealthCheckProvider : HealthControllerBase
    {
        private int _minPercentageFree = 10;
        private int _warningPercentageFree = 20;

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public DiskSpaceHealthCheckProvider() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetHealthCheckConfiguration()
        {
            base.SortOrder = 30;
            base.IsEnabled = true;

            var minPercentageFree = base.OptionSetting.SelectTokens("$.DiskSpace.MinPercentageFree");
            if (minPercentageFree.Any())
            {
                this._minPercentageFree = minPercentageFree.ElementAt(0).Value<int>();
            }
            var warningPercentageFree = base.OptionSetting.SelectTokens("$.DiskSpace.WarningPercentageFree");
            if (warningPercentageFree.Any())
            {
                this._warningPercentageFree = warningPercentageFree.ElementAt(0).Value<int>();
            }
        }

        /// <summary>
        /// Returns the health heck info.
        /// </summary>
        public override Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var result = new HealthCheckItemResult(nameof(DiskSpaceHealthCheckProvider), base.SortOrder, "當前部署環境下磁盤空間檢查", $"验证可用磁盘空间是否大于 {_minPercentageFree}%.");
            try
            {
                var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var percentageFree = this.GetPercentageFree(drive);
                result.HealthState = this.DetermineState(percentageFree);
                result.Messages.Add($"可用磁盤空間{(percentageFree > _minPercentageFree ? "大於" : "小於")} {_minPercentageFree}%.");
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
        private double GetPercentageFree(string drive)
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
        private HealthState DetermineState(double percentageFree)
        {
            if (percentageFree < _minPercentageFree)
            {
                return HealthState.Unhealthy;
            }
            if (percentageFree < _warningPercentageFree)
            {
                return HealthState.Degraded;
            }
            return HealthState.Healthy;
        }
    }
}