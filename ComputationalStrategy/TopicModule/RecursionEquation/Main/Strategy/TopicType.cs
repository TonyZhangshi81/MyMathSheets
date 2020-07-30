using System;

namespace MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Strategy
{
	/// <summary>
	/// 題型編號
	/// </summary>
	[Flags]
	public enum TopicType : int
	{
		/// <summary>
		/// A+B+C
		/// </summary>
		CleverA = 40,

		/// <summary>
		/// A-(B-C)
		/// </summary>
		CleverB = 41,

		/// <summary>
		/// A-(B+C)
		/// </summary>
		CleverC = 42,

		/// <summary>
		/// A+(B-C)
		/// </summary>
		CleverD = 43,

		/// <summary>
		/// A+(B+C)
		/// </summary>
		CleverE = 44,

		/// <summary>
		/// A+B-C
		/// </summary>
		CleverF = 45,

		/// <summary>
		/// A-B+C
		/// </summary>
		CleverG = 46,

		/// <summary>
		/// A-B-C
		/// </summary>
		CleverH = 47
	}
}