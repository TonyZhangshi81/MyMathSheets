using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MultArithmetic.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.MultArithmetic.Main.Parameters
{
	/// <summary>
	/// 多元四則運算參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.MultArithmetic)]
	public class MultArithmeticParameter : ParameterBase
	{
		/// <summary>
		/// 多元四則運算作成并輸出
		/// </summary>
		public IList<MultArithmeticFormula> Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 多元四則運算結合實例化
			Formulas = new List<MultArithmeticFormula>();
		}
	}
}
