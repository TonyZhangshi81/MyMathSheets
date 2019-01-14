using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
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
					Console.WriteLine(string.Format("{0}{1} {2} {3} = {4}",
						d.Arithmetic.IsNeedBracket ? "(" : string.Empty,
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						// 是否運用多級四則運算
						(d.MultistageArithmetic is null) 
							? CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap) + (d.Arithmetic.IsNeedBracket ? ")" : string.Empty) : GetMultistageFormula(d.Arithmetic, d.MultistageArithmetic),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1}{2} {3} {4}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap),
						d.Arithmetic.IsNeedBracket ? "(" : string.Empty,
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						// 是否運用多級四則運算
						(d.MultistageArithmetic is null) 
							? CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.RightParameter, d.Arithmetic.Gap) + (d.Arithmetic.IsNeedBracket ? ")" : string.Empty) : GetMultistageFormula(d.Arithmetic, d.MultistageArithmetic)));
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
			return string.Format("{0}{1}{2} {3} {4}{5}",
				multistageFormula.IsNeedBracket ? "(" : string.Empty,
				CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, multistageFormula.LeftParameter, multistageFormula.Gap),
				leftFormula.IsNeedBracket ? ")" : string.Empty,
				multistageFormula.Sign.ToOperationString(),
				// 前一級運算符是減法的話,下一級的運算符需要變換
				//(leftFormula.Sign == CommonLib.Util.SignOfOperation.Subtraction) 
				//	? (multistageFormula.Sign == CommonLib.Util.SignOfOperation.Plus) 
				//		? CommonLib.Util.SignOfOperation.Subtraction.ToOperationString() : CommonLib.Util.SignOfOperation.Plus.ToOperationString() : multistageFormula.Sign.ToOperationString(),
				CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, multistageFormula.RightParameter, multistageFormula.Gap),
				multistageFormula.IsNeedBracket ? ")" : string.Empty);
		}
	}
}
