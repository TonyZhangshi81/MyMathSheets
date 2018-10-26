using System;

namespace MyMathSheets.TheFormulaShows.Attributes
{
	/// <summary>
	/// 
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class SubstituteAttribute : Attribute
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly string _source;
		/// <summary>
		/// 
		/// </summary>
		private readonly string _target;
		/// <summary>
		/// 
		/// </summary>
		public string Source
		{
			get { return _source; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Target
		{
			get { return _target; }
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		public SubstituteAttribute(string source, string target)
		{
			_source = source;
			_target = target;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Substitute
	{
		/// <summary>
		/// 
		/// </summary>
		public string Souce { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Target { get; set; }
	}
}
