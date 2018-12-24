using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Parameters
{
	/// <summary>
	/// 認識長度參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.LearnLengthUnit)]
	public class LearnLengthUnitParameter : ParameterBase
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
			base.InitParameter();

			object value = JsonExtension.GetPropertyByJson(Reserve, "Type");
			Types = Convert.ToString(value).Split(',').Select(s => int.Parse(s)).ToArray();

			// 認識長度集合實例化
			Formulas = new List<LearnLengthUnitFormula>();
		}
	}
}
