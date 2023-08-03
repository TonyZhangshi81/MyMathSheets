using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Strategy;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.RecursionEquation.Item
{
	/// <summary>
	/// 計算式對象構成
	/// </summary>
	public class RecursionEquationFormula
	{
		/// <summary>
		/// 題型編號
		/// </summary>
		public TopicType Type { get; set; }

		/// <summary>
		/// 計算式集合
		/// </summary>
		public IList<Formula> ConfixFormulas { get; set; }

		/// <summary>
		/// 解題答案
		/// </summary>
		public List<int> Answer { get; set; }
	}
}