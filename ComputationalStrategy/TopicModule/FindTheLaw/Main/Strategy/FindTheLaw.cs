using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Strategy
{
	/// <summary>
	/// 找規律題
	/// </summary>
	[Operation(LayoutSetting.Preview.FindTheLaw)]
	public class FindTheLaw : OperationBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter"></param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			FindTheLawParameter p = parameter as FindTheLawParameter;


		}




	}
}
