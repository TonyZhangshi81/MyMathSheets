using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item
{
	/// <summary>
	/// 認識價格
	/// </summary>
	public class CurrencyLinkageFormula
	{
		/// <summary>
		/// 左邊计算式集合
		/// </summary>
		public IList<Formula> LeftFormulas { get; set; }

		/// <summary>
		/// 右邊计算式集合
		/// </summary>
		public IList<Formula> RightFormulas { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public IList<int> Sort { get; set; }

		/// <summary>
		/// 容器的擺放位置（從0開始）
		/// </summary>
		public IList<int> Seats { get; set; }
	}
}
