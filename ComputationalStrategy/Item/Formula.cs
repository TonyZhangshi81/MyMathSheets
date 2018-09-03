using CommonLib.Util;

namespace ComputationalStrategy.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class Formula
	{
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
		/// <see cref="Item.SignOfOperation"/>
		public SignOfOperation Sign { get; set; }
		/// <summary>
		/// 等式结果
		/// </summary>
		public int Answer { get; set; }
		/// <summary>
		/// 填空随机项目（运算符左边参数、运算符右边参数、等式结果）
		/// </summary>
		public GapFilling Gap { get; set; }
	}
}
