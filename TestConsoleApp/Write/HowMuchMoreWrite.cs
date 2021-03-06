﻿using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Item;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 比多少题型计算式结果显示输出
	/// </summary>
	[TopicWrite("HowMuchMore")]
	public class HowMuchMoreWrite : TopicWriteBase<HowMuchMoreParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<HowMuchMoreFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "比多少"));

			formulas.ToList().ForEach(d =>
			{
				var formula = d.DefaultFormula;
				Console.WriteLine(string.Format("{0}-{1}={2}", formula.LeftParameter, formula.RightParameter, formula.Answer));

				Console.WriteLine(d.MathWordProblem);

				var left = "@".PadLeft(formula.LeftParameter, '@');
				var right = "#".PadLeft(formula.RightParameter, '#');
				Console.WriteLine("{0}   {1}", left, right);

				Console.WriteLine(string.Format("顯示項目：{0}", d.DisplayLeft ? left : right));
				Console.WriteLine(string.Format("顯示答案：{0}", d.DisplayLeft ? right : left));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(HowMuchMoreParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}