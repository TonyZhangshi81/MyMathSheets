using System.Runtime.Serialization;

namespace ComputationalStrategy.Item
{
	public class MathWordProblemsFormula
	{
		/// <summary>
		/// 
		/// </summary>
		public Formula ProblemFormula { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MathWordProblem { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Verify { get; set; }
	}

	[DataContract]
	public class Problems
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
