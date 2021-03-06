﻿using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.FindTheLaw.Item
{
	/// <summary>
	/// 題型計算式對象構成（暫定標準：7個數字，其中3個為填空項目）
	/// </summary>
	public class FindTheLawFormula
	{
		/// <summary>
		/// 自然數列表
		/// </summary>
		public List<int> NumberList { get; set; }

		/// <summary>
		/// 隨機項目編號(填空項目)
		/// </summary>
		public List<int> RandomIndexList { get; set; }
	}
}