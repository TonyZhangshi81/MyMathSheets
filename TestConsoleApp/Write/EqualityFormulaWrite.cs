using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.Write
{
	/// <summary>
	/// 
	/// </summary>
	public class EqualityFormulaWrite : IConsoleWrite<List<EqualityFormula>>
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
					d.LeftFormula.LeftParameter,
					d.LeftFormula.Sign.ToOperationString(),
					d.LeftFormula.RightParameter,
					d.Answer.ToSignOfCompareString(),
					d.RightFormula.LeftParameter,
					d.RightFormula.Sign.ToOperationString(),
					d.RightFormula.RightParameter));
			});
		}

	}
}
