using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 填空題题型计算式结果显示输出
	/// </summary>
	public class GapFillingProblemsWrite : IConsoleWrite
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<GapFillingProblemsFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "基礎填空題"));

			int index = 0;
			StringBuilder content = new StringBuilder();
			formulas.ToList().ForEach(d =>
			{
				// 填空題內容中的參數拼接
				Regex.Split(d.GapFillingProblem, "INPUT").ToList().ForEach(m =>
				{
					content.Append(m).Append(d.Answers[index]);
					index++;
				});
				// 內容打印
				Console.WriteLine(string.Format("{0}  LEVEL<{1}>", content, d.Level));
				content.Length = 0;
				index = 0;
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public void ConsoleFormulas(TopicParameterBase parameter)
		{
			GapFillingProblemsParameter param = (GapFillingProblemsParameter)parameter;

			ConsoleFormulas(param.Formulas);
		}
	}
}