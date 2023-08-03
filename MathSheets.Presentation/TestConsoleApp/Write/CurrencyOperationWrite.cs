using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.CurrencyOperation.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
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
	/// 貨幣運算题型计算式结果显示输出
	/// </summary>
	[TopicWrite("CurrencyOperation")]
	public class CurrencyOperationWrite : TopicWriteBase<CurrencyOperationParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<CurrencyOperationFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "貨幣運算"));

			formulas.ToList().ForEach(d =>
			{
				if (d.AnswerIsRight)
				{
					Console.WriteLine(string.Format("{0} {1} {2} = {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.CurrencyArithmetic.LeftParameter.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap),
						d.CurrencyArithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.CurrencyArithmetic.RightParameter.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.CurrencyArithmetic.Answer.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1} {2} {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.CurrencyArithmetic.Answer.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.CurrencyArithmetic.LeftParameter.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap),
						d.CurrencyArithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.CurrencyArithmetic.RightParameter.CurrencyOperationUnitTypeToString(), d.CurrencyArithmetic.Gap)));
				}
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(CurrencyOperationParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}