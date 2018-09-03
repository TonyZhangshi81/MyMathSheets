using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
	/// <summary>
	/// 
	/// </summary>
	public class MathWordProblemsFormulaWrite : IConsoleWrite<List<MathWordProblemsFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<MathWordProblemsFormula> formulas)
		{
			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1}", d.MathWordProblem, d.Verify));
			});
		}

	}
}
