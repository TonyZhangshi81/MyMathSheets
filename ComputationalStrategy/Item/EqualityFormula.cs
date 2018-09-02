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

	/// <summary>
	/// 比较运算符
	/// </summary>
	public enum SignOfCompare : int
	{
		/// <summary>
		/// 大于
		/// </summary>
		Greater = 0,
		/// <summary>
		/// 小于
		/// </summary>
		Less,
		/// <summary>
		/// 等于
		/// </summary>
		Equal
	}
}
