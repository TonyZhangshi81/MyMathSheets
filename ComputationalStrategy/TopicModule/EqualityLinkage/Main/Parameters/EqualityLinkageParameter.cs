﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Item;
using System;

namespace MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters
{
	/// <summary>
	/// 算式連一連參數類
	/// </summary>
	[TopicParameter("EqualityLinkage")]
	public class EqualityLinkageParameter : TopicParameterBase
	{
		/// <summary>
		/// 算式連一連作成并輸出
		/// </summary>
		public EqualityLinkageFormula Formulas { get; set; }

		/// <summary>
		/// 是否為縱向排列(0:橫向 1:縱向)
		/// </summary>
		public DivQueueType QueueType { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 是否為縱向排列
			QueueType = (JsonExtension.GetPropertyByJson(Reserve, "DivQueueType") == null) ? DivQueueType.Lengthways : (DivQueueType)Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "DivQueueType"));

			// 集合實例化
			Formulas = new EqualityLinkageFormula();
		}
	}
}