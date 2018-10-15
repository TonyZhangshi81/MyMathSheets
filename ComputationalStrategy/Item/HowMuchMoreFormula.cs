using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.Item
{
	/// <summary>
	/// 比多少
	/// </summary>
	public class HowMuchMoreFormula
	{
		/// <summary>
		/// 计算式（限制只能使用減法運算符）
		/// </summary>
		public Formula DefaultFormula { get; set; }

		/// <summary>
		/// 當前顯示的項目
		/// </summary>
		public HowMuchMoreType LeftOrRightParameter { get; set; }

		/// <summary>
		/// 比多少題型的文字表述部分(eg:左邊項目比右邊項目多1個)
		/// </summary>
		public string MathWordProblem { get; set; }

		/// <summary>
		/// 答案值
		/// </summary>
		public int Answer { get; set; }
	}
}
