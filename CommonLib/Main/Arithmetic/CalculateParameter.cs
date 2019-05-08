using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 計算式參數類
	/// </summary>
	public class CalculateParameter
	{
		/// <summary>
		/// 推算範圍最大值（基於answer的推算）
		/// </summary>
		public int MaximumLimit { get; set; }

		/// <summary>
		/// 推算範圍最小值（基於answer的推算）
		/// </summary>
		public int MinimumLimit { get; set; }

		/// <summary>
		/// 題型（標準填空、隨機填空）
		/// </summary>
		public QuestionType QuestionType { get; set; }

		/// <summary>
		/// 推算範圍（基於算式第一參數指定範圍的推算）
		/// </summary>
		public int[] LeftScope { get; set; }

		/// <summary>
		/// 推算範圍（基於算式第二參數指定範圍的推算）
		/// </summary>
		public int[] RightScope { get; set; }
	}
}
