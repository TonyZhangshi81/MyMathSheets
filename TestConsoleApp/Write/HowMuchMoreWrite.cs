using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 
	/// </summary>
	public class HowMuchMoreWrite : IConsoleWrite<List<HowMuchMoreFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<HowMuchMoreFormula> formulas)
		{
			formulas.ToList().ForEach(d =>
			{
				var formula = d.DefaultFormula;
				Console.WriteLine(string.Format("{0}-{1}={2}", formula.LeftParameter, formula.RightParameter, formula.Answer));

				Console.WriteLine(d.MathWordProblem);

				var left = "@".PadLeft(formula.LeftParameter, '@');
				var right = "#".PadLeft(formula.RightParameter, '#');
				Console.WriteLine("{0}   {1}", left, right);

				Console.WriteLine(string.Format("顯示項目：{0}", (d.LeftOrRightParameter == HowMuchMoreType.Left) ? left : right));
				Console.WriteLine(string.Format("顯示答案：{0}", (d.LeftOrRightParameter == HowMuchMoreType.Left) ? right : left));
			});
		}
	}
}
