using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 四則運算题型计算式结果显示输出
	/// </summary>
	public class FormulaWrite : IConsoleWrite<List<ArithmeticFormula>>
	{
		private static readonly Log log = Log.LogReady(typeof(FormulaWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<ArithmeticFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "四則運算"));

			formulas.ToList().ForEach(d =>
			{
				if (d.AnswerIsRight)
				{
					Console.WriteLine(string.Format("{0} {1} {2} = {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1} {2} {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap)));
				}
			});
		}
	}
}