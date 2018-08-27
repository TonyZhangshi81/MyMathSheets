using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Item
{
	/// <summary>
	/// 
	/// </summary>
	public class Formula
	{
		/// <summary>
		/// 
		/// </summary>
		public int LeftParameter { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int RightParameter { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Operation SignOfOperation { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int Answer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public GapFilling Gap { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public enum Operation : int
	{
		/// <summary>
		/// 
		/// </summary>
		add = 0,
		/// <summary>
		/// 
		/// </summary>
		subtraction,
		/// <summary>
		/// 
		/// </summary>
		multiplication,
		/// <summary>
		/// 
		/// </summary>
		division
	}
	/// <summary>
	/// 
	/// </summary>
	public enum GapFilling : int
	{
		/// <summary>
		/// 
		/// </summary>
		Left = 0,
		/// <summary>
		/// 
		/// </summary>
		Right,
		/// <summary>
		/// 
		/// </summary>
		Answer
	}
	/// <summary>
	/// 
	/// </summary>
	public enum QuestionTypes : int
	{
		/// <summary>
		/// 
		/// </summary>
		Standard = 0,
		/// <summary>
		/// 
		/// </summary>
		GapFilling
	}
}
