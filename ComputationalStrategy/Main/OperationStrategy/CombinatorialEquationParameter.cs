using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 組合計算式參數類
	/// </summary>
	public class CombinatorialEquationParameter : ParameterBase
	{
		/// <summary>
		/// 組合計算式作成并輸出
		/// </summary>
		public IList<CombinatorialFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 組合計算式集合實例化
			Formulas = new List<CombinatorialFormula>();
		}
	}
}
