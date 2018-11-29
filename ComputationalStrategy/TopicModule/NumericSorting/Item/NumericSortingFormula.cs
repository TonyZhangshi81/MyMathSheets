using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.NumericSorting.Item
{
	/// <summary>
	/// 題型計算式對象構成
	/// </summary>
	public class NumericSortingFormula
	{
		/// <summary>
		/// 排序數列
		/// </summary>
		public List<int> NumberList { get; set; }
		/// <summary>
		/// 答案數列
		/// </summary>
		public List<int> AnswerList { get; set; }
		/// <summary>
		/// 大於小於符號
		/// </summary>
		public SignOfCompare Sign { get; set; }
	}
}
