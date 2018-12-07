using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 計算式參數類
	/// </summary>
	public class CalculateParameter
	{
		/// <summary>
		/// 推算範圍最大值
		/// </summary>
		public int MaximumLimit { get; set; }

		/// <summary>
		/// 題型（標準填空、隨機填空）
		/// </summary>
		public QuestionType QuestionType { get; set; }

		/// <summary>
		/// 推算範圍最小值
		/// </summary>
		public int MinimumLimit { get; set; }
	}
}
