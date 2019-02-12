using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters
{
	/// <summary>
	/// 豎式計算參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.MathUpright)]
	public class MathUprightParameter : ParameterBase
	{
		/// <summary>
		/// 豎式計算作成并輸出
		/// </summary>
		public IList<MathUprightFormula> Formulas { get; set; }
		/// <summary>
		/// 是否有多級運算
		/// </summary>
		public bool Multistage { get; set; }
		/// <summary>
		/// 是否使用括號（小括號）
		/// </summary>
		public bool IsNeedBracket { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			Multistage = (JsonExtension.GetPropertyByJson(Reserve, "Multistage") == null) ? false : Convert.ToBoolean(JsonExtension.GetPropertyByJson(Reserve, "Multistage"));

			// 如果不是多集計算式，則小括號無效
			if (Multistage)
			{
				IsNeedBracket = (JsonExtension.GetPropertyByJson(Reserve, "Bracket") == null) ? false : Convert.ToBoolean(JsonExtension.GetPropertyByJson(Reserve, "Bracket"));
			}
			else
			{
				IsNeedBracket = false;
			}

			// 豎式計算結合實例化
			Formulas = new List<MathUprightFormula>();
		}
	}
}
