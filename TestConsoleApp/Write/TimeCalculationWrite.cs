using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 時間運算题型计算式结果显示输出
	/// </summary>
	[TopicWrite("TimeCalculation")]
	public class TimeCalculationWrite : TopicWriteBase<TimeCalculationParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<TimeCalculationFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "時間運算"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0}在{1}{2}是{3}",
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, string.Format("{0}:{1}:{2}", d.StartTime.Hours, d.StartTime.Minutes, d.StartTime.Seconds), d.Gap),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, string.Format("{0}:{1}:{2}", d.ElapsedTime.Hours, d.ElapsedTime.Minutes, d.ElapsedTime.Seconds), d.Gap),
					d.Sign.ToOperationString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, string.Format("{0}:{1}:{2}", d.EndTime.Hours, d.EndTime.Minutes, d.EndTime.Seconds), d.Gap)
					));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(TimeCalculationParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}