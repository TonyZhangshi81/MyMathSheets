﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters
{
	/// <summary>
	/// 比多少參數類
	/// </summary>
	[TopicParameter("HowMuchMore")]
	public class HowMuchMoreParameter : TopicParameterBase
	{
		/// <summary>
		/// 比多少作成并輸出
		/// </summary>
		public IList<HowMuchMoreFormula> Formulas { get; set; }

		/// <summary>
		/// 智能提示
		/// </summary>
		public HelperDialogue BrainpowerHint { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 集合實例化
			Formulas = new List<HowMuchMoreFormula>();
		}
	}
}