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
		/// 綜合題型巧算
		/// </summary>
		Synthetic = 3
	}

	/// <summary>
	/// 綜合題型巧算
	/// </summary>
	[Flags]
	public enum Synthetic : int
	{
		/// <summary>
		/// 拆解
		/// </summary>
		Unknit = 30,

		/// <summary>
		/// 合併
		/// </summary>
		Combine = 31
	}
}