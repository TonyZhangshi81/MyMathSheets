using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyMathSheets.ComputationalStrategy.GapFillingProblems.Item
{
	/// <summary>
	/// 題型參數類
	/// </summary>
	public class GapFillingProblemsFormula
	{
		/// <summary>
		/// 填空題文字內容
		/// </summary>
		public string GapFillingProblem { get; set; }

		/// <summary>
		/// 級別難易度（1至5級，級別越高難度越大）
		/// </summary>
		[DataMember(Name = "level")]
		public int Level { get; set; }

		/// <summary>
		/// 答案集合
		/// </summary>
		public List<string> Answers { get; set; }

		/// <summary>
		/// 參數集合
		/// </summary>
		public List<string> Parameters { get; set; }
	}

	/// <summary>
	/// 填空題題型類
	/// </summary>
	[DataContract]
	public class GapFillingProblemsLibrary
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
		/// 級別難易度（1至5級，級別越高難度越大）
		/// </summary>
		[DataMember(Name = "level")]
		public int Level { get; set; }

		/// <summary>
		/// 參數集合
		/// </summary>
		[DataMember(Name = "parameters")]
		public List<string> Parameters { get; set; }

		/// <summary>
		/// 答案集合
		/// </summary>
		[DataMember(Name = "answers")]
		public List<string> Answers { get; set; }
	}
}