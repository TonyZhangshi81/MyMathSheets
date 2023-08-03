using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 算式應用題题型计算式结果显示输出
	/// </summary>
	[TopicWrite("MathWordProblems")]
	public class MathWordProblemsFormulaWrite : TopicWriteBase<MathWordProblemsParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<MathWordProblemsFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "算式應用題"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} 答題：{1}", d.MathWordProblem, d.Answers[0], d.Unit)
					+ (String.IsNullOrEmpty(d.Unit) ? string.Empty : string.Format("({0})", d.Unit)));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(MathWordProblemsParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}