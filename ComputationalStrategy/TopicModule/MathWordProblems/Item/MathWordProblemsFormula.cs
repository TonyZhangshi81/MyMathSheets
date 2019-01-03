using MyMathSheets.CommonLib.Main.Item;
using System.Runtime.Serialization;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Item
{
	/// <summary>
	/// 題型參數類
	/// </summary>
	public class MathWordProblemsFormula
	{
		/// <summary>
		/// 計算方程式
		/// </summary>
		public Formula ProblemFormula { get; set; }
		/// <summary>
		/// 應用題文字內容
		/// </summary>
		public string MathWordProblem { get; set; }
		/// <summary>
		/// 標準計算式
		/// </summary>
		public string Verify { get; set; }
		/// <summary>
		/// 單位Verify
		/// </summary>
		public string Unit { get; set; }
	}

	/// <summary>
	/// 應用題題型類
	/// </summary>
	[DataContract]
	public class Problems
	{
		/// <summary>
		/// 編號
		/// </summary>
		[DataMember(Name = "id")]
		public string ID { get; set; }

		/// <summary>
		/// 內容
		/// </summary>
		[DataMember(Name = "content")]
		public string Content { get; set; }

		/// <summary>
		/// 計算式
		/// </summary>
		[DataMember(Name = "verify")]
		public string Verify { get; set; }

		/// <summary>
		/// 單位
		/// </summary>
		[DataMember(Name = "unit")]
		public string Unit { get; set; }
	}
}
