using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters
{
	/// <summary>
	/// 填空題參數類
	/// </summary>
	[TopicParameter("GapFillingProblems")]
	public class GapFillingProblemsParameter : TopicParameterBase
	{
		/// <summary>
		/// 填空題作成并輸出
		/// </summary>
		public IList<GapFillingProblemsFormula> Formulas { get; set; }

		/// <summary>
		/// 基礎填空題型參數設置（級別選擇）
		/// </summary>
		public int[] Levels { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			object value = JsonExtension.GetPropertyByJson(Reserve, "Level");
			Levels = Convert.ToString(value).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();

			// 集合實例化
			Formulas = new List<GapFillingProblemsFormula>();
		}
	}
}