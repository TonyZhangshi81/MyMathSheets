using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 豎式計算题型计算式结果显示输出
	/// </summary>
	public class MathUprightWrite : IConsoleWrite<List<MathUprightFormula>>
	{
		private static Log log = Log.LogReady(typeof(MathUprightWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">豎式計算题型计算式</param>
		public void ConsoleFormulas(List<MathUprightFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "豎式計算"));

			formulas.ToList().ForEach(d =>
			{

			});
		}
	}
}
