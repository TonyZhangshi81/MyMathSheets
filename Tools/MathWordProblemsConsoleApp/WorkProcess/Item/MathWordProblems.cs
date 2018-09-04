using System.Runtime.Serialization;

namespace MathWordProblemsConsoleApp.WorkProcess.Item
{
	/// <summary>
	/// 
	/// </summary>
	[DataContract]
	public class MathWordProblems
	{
		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "id")]
		public string ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "content")]
		public string Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "verify")]
		public string Verify { get; set; }
	}
}
