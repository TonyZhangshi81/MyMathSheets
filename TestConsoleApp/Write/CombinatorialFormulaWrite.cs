using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Item;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 算式組合题型计算式结果显示输出
	/// </summary>
	public class CombinatorialFormulaWrite : IConsoleWrite<List<CombinatorialFormula>>
	{
		private static Log log = Log.LogReady(typeof(CombinatorialFormulaWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">算式組合题型计算式</param>
		public void ConsoleFormulas(List<CombinatorialFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "算式組合"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} {3}", d.ParameterA, d.ParameterB, d.ParameterC, d.ParameterD));
				d.CombinatorialFormulas.ToList().ForEach(m =>
				{
					Console.WriteLine(string.Format("{0} {1} {2} = {3}",
										m.LeftParameter,
										m.Sign.ToOperationString(),
										m.RightParameter,
										m.Answer));
				});
			});
		}
	}
}
