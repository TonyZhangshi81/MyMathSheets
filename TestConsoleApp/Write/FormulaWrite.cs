using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TestConsoleApp.Properties;
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
		private static Log log = Log.LogReady(typeof(FormulaWrite));

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
						Util.GetValue(GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						Util.GetValue(GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap),
						Util.GetValue(GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1} {2} {3}",
						Util.GetValue(GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap),
						Util.GetValue(GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						Util.GetValue(GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap)));
				}
			});
		}
	}
}
