using System;

namespace MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Strategy
{
	/// <summary>
	/// 題型編號
	/// </summary>
	[Flags]
	public enum TopicType : int
	{
		/// <summary>
		/// 一搬加法巧算
		/// </summary>
		Plus = 0,

		/// <summary>
		/// 一般減法巧算
		/// </summary>
		Subtraction = 1,

		/// <summary>
		/// 一般乘法巧算
		/// </summary>
		Multiple = 2,

		/// <summary>
		/// 綜合題型巧算(拆解)
		/// </summary>
		SyntheticUnknit = 3,

		/// <summary>
		/// 綜合題型巧算(合併)
		/// </summary>
		SyntheticCombine = 4,
	}
}