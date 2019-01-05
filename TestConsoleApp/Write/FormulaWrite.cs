using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
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
						CommonUtil.GetValue(GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						// 是否運用多級四則運算
						(d.MultistageArithmetic is null) ? CommonUtil.GetValue(GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap) : GetMultistageFormula(d.Arithmetic, d.MultistageArithmetic),
						CommonUtil.GetValue(GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1} {2} {3}",
						CommonUtil.GetValue(GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap),
						CommonUtil.GetValue(GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						// 是否運用多級四則運算
						(d.MultistageArithmetic is null) ? CommonUtil.GetValue(GapFilling.Left, d.Arithmetic.RightParameter, d.Arithmetic.Gap) : GetMultistageFormula(d.Arithmetic, d.MultistageArithmetic)));
				}
			});
		}

		/// <summary>
		/// 第二級四則運算打印顯示
		/// </summary>
		/// <param name="leftFormula">前一級運算式</param>
		/// <param name="multistageFormula">第二級運算式</param>
		/// <returns>四則運算打印顯示信息</returns>
		private string GetMultistageFormula(Formula leftFormula, Formula multistageFormula)
		{
			return string.Format("{0} {1} {2}",
				CommonUtil.GetValue(GapFilling.Right, multistageFormula.LeftParameter, multistageFormula.Gap),
				// 前一級運算符是減法的話,下一級的運算符需要變換
				(leftFormula.Sign == SignOfOperation.Subtraction) ? (multistageFormula.Sign == SignOfOperation.Plus) ?  SignOfOperation.Subtraction.ToOperationString() : SignOfOperation.Plus.ToOperationString() : multistageFormula.Sign.ToOperationString(),
				CommonUtil.GetValue(GapFilling.Left, multistageFormula.RightParameter, multistageFormula.Gap));
		}
	}
}
