using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 時間運算题型计算式结果显示输出
	/// </summary>
	public class TimeCalculationWrite : IConsoleWrite<List<TimeCalculationFormula>>
	{
		private static Log log = Log.LogReady(typeof(TimeCalculationWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<TimeCalculationFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "時間運算"));

			formulas.ForEach(d =>
			{
				Console.WriteLine(string.Format("{0}在{1}{2}是{3}",
					string.Format("{0}:{1}:{2}", d.StartTime.Hours, d.StartTime.Minutes, d.StartTime.Seconds),
					string.Format("{0}:{1}:{2}", d.ElapsedTime.Hours, d.ElapsedTime.Minutes, d.ElapsedTime.Seconds),
					d.Sign.ToOperationString(),
					string.Format("{0}:{1}:{2}", d.EndTime.Hours, d.EndTime.Minutes, d.EndTime.Seconds)));
			});
		}
	}
}
