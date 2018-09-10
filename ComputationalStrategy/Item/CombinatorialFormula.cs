﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Item
{
	/// <summary>
	/// 組合算式的結構對象
	/// </summary>
	public class CombinatorialFormula
	{
		/// <summary>
		/// 第一個參數
		/// </summary>
		public int ParameterA { get; set; }
		/// <summary>
		/// 第二個參數
		/// </summary>
		public int ParameterB { get; set; }
		/// <summary>
		/// 第三個參數
		/// </summary>
		public int ParameterC { get; set; }
		/// <summary>
		/// 組合后的算式集合
		/// </summary>
		public IList<Formula> CombinatorialFormulas { get; set; }
	}
}
