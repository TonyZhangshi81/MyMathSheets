using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper
{
	/// <summary>
	/// 四則運算題型的智能提示作成類
	/// </summary>
	public class ArithmeticDialogue : VirtualHelperBase<ArithmeticFormula>
	{
		/// <summary>
		/// 智能提示會話內容作成
		/// </summary>
		/// <param name="formulaIndex">會話內容所對應題目的序號</param>
		/// <returns>會話內容列表</returns>
		protected override List<string> CreateDialogue(List<int> formulaIndex)
		{
			// TODO

			return new List<string>();
		}
	}
}
