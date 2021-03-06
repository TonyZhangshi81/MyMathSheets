﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.Provider;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters
{
	/// <summary>
	/// 應用題參數類
	/// </summary>
	[TopicParameter("MathWordProblems")]
	public class MathWordProblemsParameter : TopicParameterBase
	{
		/// <summary>
		/// 應用題作成并輸出
		/// </summary>
		public IList<MathWordProblemsFormula> Formulas { get; set; }

		/// <summary>
		/// 基礎填空題型參數設置（級別選擇）
		/// </summary>
		public int[] Levels { get; set; }

		/// <summary>
		/// 應用題庫文件所在路徑
		/// </summary>
		/// <remarks>
		/// 由補充參數設定<see cref="JsonTopicParameterProvider"/>提供設定值
		/// </remarks>
		public string ProblemsJsonFilePath { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			object value = JsonExtension.GetPropertyByJson(Reserve, "Level");
			Levels = Convert.ToString(value).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();

			// 集合實例化
			Formulas = new List<MathWordProblemsFormula>();
		}
	}
}