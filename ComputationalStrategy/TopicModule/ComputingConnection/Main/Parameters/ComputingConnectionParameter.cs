﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Item;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters
{
	/// <summary>
	/// 等式接龍參數類
	/// </summary>
	[TopicParameter("ComputingConnection")]
	public class ComputingConnectionParameter : TopicParameterBase
	{
		/// <summary>
		/// 等式接龍作成并輸出
		/// </summary>
		public IList<ConnectionFormula> Formulas { get; set; }

		/// <summary>
		/// 層數設置
		/// </summary>
		public int SectionNumber { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			SectionNumber = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "SectionNumber"));

			// 等式接龍集合實例化
			Formulas = new List<ConnectionFormula>();
		}
	}
}