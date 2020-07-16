using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.SchoolClock.Item;
using MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters;
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
	public class SchoolClockWrite : IConsoleWrite
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<SchoolClockFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "時鐘學習板"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1}:{2}:{3} {4}",
								d.LatestTime.TimeInterval.TimeIntervalTypeToString().PadRight(18),
								d.LatestTime.Hours.ToString().PadLeft(2, '0'),
								d.LatestTime.Minutes.ToString().PadLeft(2, '0'),
								d.LatestTime.Seconds.ToString().PadLeft(2, '0'),
								d.LatestTime.GetTimeType.ToString()));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public void ConsoleFormulas(TopicParameterBase parameter)
		{
			SchoolClockParameter param = (SchoolClockParameter)parameter;

			ConsoleFormulas(param.Formulas);
		}
	}
}