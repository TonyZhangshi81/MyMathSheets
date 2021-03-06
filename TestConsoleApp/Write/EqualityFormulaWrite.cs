﻿using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Item;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Main.Parameters;
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
	/// 運算比大小题型计算式结果显示输出
	/// </summary>
	[TopicWrite("EqualityComparison")]
	public class EqualityFormulaWrite : TopicWriteBase<EqualityComparisonParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<EqualityFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "運算比大小"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}",
					d.LeftFormula.LeftParameter,
					d.LeftFormula.Sign.ToOperationString(),
					d.LeftFormula.RightParameter,
					d.Answer.ToSignOfCompareString(),
					d.RightFormula.LeftParameter,
					d.RightFormula.Sign.ToOperationString(),
					d.RightFormula.RightParameter));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(EqualityComparisonParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}