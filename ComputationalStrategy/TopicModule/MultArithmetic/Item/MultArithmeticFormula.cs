using MyMathSheets.CommonLib.Main.Item;

namespace MyMathSheets.ComputationalStrategy.MultArithmetic.Item
{
	/// <summary>
	/// 计算式对象二叉樹构成
	/// </summary>
	public class MultArithmeticFormula
	{
		/// <summary>
		/// 根計算式
		/// </summary>
		public Formula Arithmetic { get; set; }

		/// <summary>
		/// 左葉計算式
		/// </summary>
		public MultArithmeticFormula LeftArithmetic { get; set; }

		/// <summary>
		/// 右葉計算式
		/// </summary>
		public MultArithmeticFormula RightArithmetic { get; set; }
	}
}
