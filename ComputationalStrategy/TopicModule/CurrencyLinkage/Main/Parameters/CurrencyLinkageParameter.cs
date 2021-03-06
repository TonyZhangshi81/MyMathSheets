﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item;
using System;

namespace MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters
{
	/// <summary>
	/// 認識價格參數類
	/// </summary>
	[TopicParameter("CurrencyLinkage")]
	public class CurrencyLinkageParameter : TopicParameterBase
	{
		/// <summary>
		/// 認識價格作成并輸出
		/// </summary>
		public CurrencyLinkageFormula Currencys { get; set; }

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
			Currencys = new CurrencyLinkageFormula();
		}
	}
}