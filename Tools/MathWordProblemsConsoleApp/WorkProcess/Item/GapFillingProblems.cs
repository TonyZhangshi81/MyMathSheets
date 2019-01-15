using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item
{
	/// <summary>
	/// 填空題題型類
	/// </summary>
	[DataContract]
	public class GapFillingProblems
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
