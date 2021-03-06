﻿using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Item;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters;
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
	/// 等式比大小题型计算式结果显示输出
	/// </summary>
	[TopicWrite("FindNearestNumber")]
	public class FindNearestNumberWrite : TopicWriteBase<FindNearestNumberParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<NearestNumberFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "等式比大小"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}",
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.LeftFormula.LeftParameter, d.LeftFormula.Gap),
					d.LeftFormula.Sign.ToOperationString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.LeftFormula.RightParameter, d.LeftFormula.Gap),
					d.Answer.ToSignOfCompareString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.RightFormula.LeftParameter, d.RightFormula.Gap),
					d.RightFormula.Sign.ToOperationString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.RightFormula.RightParameter, d.RightFormula.Gap)));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(FindNearestNumberParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}