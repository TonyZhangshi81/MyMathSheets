using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class Formula
	{
		/// <summary>
		/// 默認情況計算式有解
		/// </summary>
		public Formula() => IsNoSolution = false;

		/// <summary>
		/// 运算符左边参数
		/// </summary>
		public int LeftParameter { get; set; }
		/// <summary>
		/// 运算符右边参数
		/// </summary>
		public int RightParameter { get; set; }
		/// <summary>
		/// 运算符
		/// </summary>
		/// <see cref="SignOfOperation"/>
		public SignOfOperation Sign { get; set; }
		/// <summary>
		/// 等式结果
		/// </summary>
		public int Answer { get; set; }
		/// <summary>
		/// 填空随机项目（运算符左边参数、运算符右边参数、等式结果）
		/// </summary>
		public GapFilling Gap { get; set; }
		/// <summary>
		/// 無解方程式（一般出現在無法整除的情況，設想是此種情況下對前算式進行反推）
		/// </summary>
		public bool IsNoSolution { get; set; }
	}
}
