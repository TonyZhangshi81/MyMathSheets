using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters
{
	/// <summary>
	/// 四則運算參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.Arithmetic)]
	public class ArithmeticParameter : ParameterBase
	{
		/// <summary>
		/// 四則運算作成并輸出
		/// </summary>
		public IList<ArithmeticFormula> Formulas { get; set; }
		/// <summary>
		/// 是否有多級運算
		/// </summary>
		public bool Multistage { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			Multistage = (JsonExtension.GetPropertyByJson(Reserve, "Multistage") == null) ? false : Convert.ToBoolean(JsonExtension.GetPropertyByJson(Reserve, "Multistage"));

			// 四則運算結合實例化
			Formulas = new List<ArithmeticFormula>();
		}
	}
}
