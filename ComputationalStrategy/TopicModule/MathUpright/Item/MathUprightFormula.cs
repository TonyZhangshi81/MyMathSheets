using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.MathUpright.Item
{
	/// <summary>
	/// 計算式對象構成
	/// </summary>
	public class MathUprightFormula
	{
		/// <summary>
		/// 默認情況計算式有解
		/// </summary>
		public Formula Arithmetic { get; set; }
		/// <summary>
		/// 計算式數據鏈（用於設置填空項目）
		/// </summary>
		public List<int?> FormulaDataLink { get; set; }
		/// <summary>
		/// 填空項目的位置
		/// </summary>
		public int FillPosition { get; set; }
	}
}
