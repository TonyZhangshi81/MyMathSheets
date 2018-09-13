using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.Item
{

	/// <summary>
	/// 等式比大小題型结构对象
	/// </summary>
	public class EqualityFormula
	{
		/// <summary>
		/// 运算符左边参数
		/// </summary>
		public Formula LeftFormula { get; set; }
		/// <summary>
		/// 运算符右边参数
		/// </summary>
		public Formula RightFormula { get; set; }
		/// <summary>
		/// 运算符（比较结果）
		/// </summary>
		/// <see cref="Item.SignOfOperation"/>
		public SignOfCompare Answer { get; set; }
	}


}
