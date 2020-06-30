using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.SchoolClock.Item;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 時鐘學習板题型计算式结果显示输出
	/// </summary>
	public class SchoolClockWrite : IConsoleWrite<List<SchoolClockFormula>>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<SchoolClockFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0004T, "時鐘學習板"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1}:{2}:{3} {4}",
								d.LatestTime.TimeInterval.TimeIntervalTypeToString().PadRight(18),
								d.LatestTime.Hours.ToString().PadLeft(2, '0'),
								d.LatestTime.Minutes.ToString().PadLeft(2, '0'),
								d.LatestTime.Seconds.ToString().PadLeft(2, '0'),
								d.LatestTime.TimeType.ToString()));
			});
		}
	}
}