﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters
{
	/// <summary>
	/// 組合計算式參數類
	/// </summary>
	[TopicParameter("CombinatorialEquation")]
	public class CombinatorialEquationParameter : TopicParameterBase
	{
		/// <summary>
		/// 組合計算式作成并輸出
		/// </summary>
		public IList<CombinatorialFormula> Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 組合計算式集合實例化
			Formulas = new List<CombinatorialFormula>();
		}
	}
}