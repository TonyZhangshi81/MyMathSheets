﻿using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 算式應用題题型计算式结果显示输出
	/// </summary>
	public class MathWordProblemsFormulaWrite : IConsoleWrite<List<MathWordProblemsFormula>>
	{
		private static Log log = Log.LogReady(typeof(MathWordProblemsFormulaWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<MathWordProblemsFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "算式應用題"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2}", d.MathWordProblem, d.Verify, d.Unit));
			});
		}

	}
}
