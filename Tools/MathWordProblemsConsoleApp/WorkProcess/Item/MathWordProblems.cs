using System.Runtime.Serialization;

namespace MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item
{
	/// <summary>
	/// 應用題題型類
	/// </summary>
	[DataContract]
	public class MathWordProblems
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
