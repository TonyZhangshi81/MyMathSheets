using CommonLib.Util;
using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.Write
{
	public class FormulaWrite : IConsoleWrite<List<Formula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<Formula> formulas)
		{
			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} = {3}",
					Util.GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Answer, d.Answer, d.Gap)));
			});
		}



	}
}
