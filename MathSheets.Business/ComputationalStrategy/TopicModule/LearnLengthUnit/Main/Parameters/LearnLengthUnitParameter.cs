using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Parameters
{
	/// <summary>
	/// 認識長度參數類
	/// </summary>
	[TopicParameter("LearnLengthUnit")]
	public class LearnLengthUnitParameter : TopicParameterBase
	{
		/// <summary>
		/// 認識長度作成并輸出
		/// </summary>
		public IList<LearnLengthUnitFormula> Formulas { get; set; }

		/// <summary>
		/// 認識長度題型參數設置
		/// </summary>
		public int[] Types { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			object value = JsonExtension.GetPropertyByJson(Reserve, "Type");
			Types = Convert.ToString(value).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();

			// 認識長度集合實例化
			Formulas = new List<LearnLengthUnitFormula>();
		}
	}
}