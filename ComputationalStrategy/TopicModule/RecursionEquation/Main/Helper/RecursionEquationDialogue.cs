using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper
{
	/// <summary>
	/// 巧算題型的智能提示作成類
	/// </summary>
	public class RecursionEquationDialogue : VirtualHelperBase<RecursionEquationFormula>
	{
		/// <summary>
		/// 智能提示會話內容作成
		/// </summary>
		/// <param name="formulaIndex">會話內容所對應題目的序號</param>
		/// <returns>會話內容列表</returns>
		protected override List<string> CreateDialogue(List<int> formulaIndex)
		{
			List<string> dialogues = new List<string>();
			formulaIndex.ForEach(d =>
			{
			});

			return dialogues;
		}
	}
}