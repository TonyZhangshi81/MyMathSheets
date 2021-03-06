﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters
{
	/// <summary>
	/// 豎式計算參數類
	/// </summary>
	[TopicParameter("MathUpright")]
	public class MathUprightParameter : TopicParameterBase
	{
		/// <summary>
		/// 豎式計算作成并輸出
		/// </summary>
		public IList<MathUprightFormula> Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 豎式計算集合實例化
			Formulas = new List<MathUprightFormula>();
		}
	}
}