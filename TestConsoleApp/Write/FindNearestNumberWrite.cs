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
	public class FindNearestNumberWrite : IConsoleWrite<List<EqualityFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<EqualityFormula> formulas)
		{
			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}",
					Util.GetValue(GapFilling.Left, d.LeftFormula.LeftParameter, d.LeftFormula.Gap),
					d.LeftFormula.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.LeftFormula.RightParameter, d.LeftFormula.Gap),
					d.Answer.ToSignOfCompareString(),
					Util.GetValue(GapFilling.Left, d.RightFormula.LeftParameter, d.RightFormula.Gap),
					d.RightFormula.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.RightFormula.RightParameter, d.RightFormula.Gap)));
			});
		}
	}
}
