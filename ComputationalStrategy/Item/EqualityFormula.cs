using CommonLib.Util;

namespace ComputationalStrategy.Item
{
	public class EqualityFormula
	{
		/// <summary>
		/// 
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
