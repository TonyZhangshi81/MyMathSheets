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
	public class CombinatorialFormulaWrite : IConsoleWrite<List<CombinatorialFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<CombinatorialFormula> formulas)
		{
			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2}", d.ParameterA, d.ParameterB, d.ParameterC));
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
