using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MultArithmetic.Main.Parameters;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.MultArithmetic.Main.Strategy
{
	/// <summary>
	/// 多元四則運算題
	/// </summary>
	[Operation(LayoutSetting.Preview.MultArithmetic)]
	public class MultArithmetic : OperationBase
	{
		/// <summary>
		/// 方程式層數
		/// </summary>
		private int _multCount { get; set; }
		/// <summary>
		/// 計算式左右參數寄存棧（用於二叉樹的構建）
		/// </summary>
		private Stack<int> _stackParameter { get; set; }

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			MultArithmeticParameter p = parameter as MultArithmeticParameter;
		}
	}
}
