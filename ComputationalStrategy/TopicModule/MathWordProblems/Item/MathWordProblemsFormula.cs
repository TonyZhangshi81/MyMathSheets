﻿using MyMathSheets.CommonLib.Main.Item;
using System.Runtime.Serialization;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Item
{
	/// <summary>
	/// 
	/// </summary>
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

	/// <summary>
	/// 
	/// </summary>
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