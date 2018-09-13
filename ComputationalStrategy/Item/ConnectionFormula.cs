using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Item
{
	/// <summary>
	/// 计算式接龙
	/// </summary>
	public class ConnectionFormula
	{
		/// <summary>
		/// 计算式集合
		/// </summary>
		public IList<Formula> ConfixFormulas { get; set; }
		/// <summary>
		/// 等式接龙的层数
		/// </summary>
		public int ConfixNumber { get; set; }
	}
}
