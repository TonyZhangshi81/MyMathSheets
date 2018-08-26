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
}
