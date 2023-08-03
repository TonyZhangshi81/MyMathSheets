using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Item;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Properties;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper
{
	/// <summary>
	/// 四則運算題型的智能提示作成類
	/// </summary>
	public class ArithmeticOperationsDialogue : VirtualHelperBase<ArithmeticOperationsFormula>
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
				DialogueType type = CommonUtil.GetRandomNumber(DialogueType.General, DialogueType.ResultHelper);
				switch (type)
				{
					case DialogueType.General:
						dialogues.Add(MsgResources.AC001);
						break;

					case DialogueType.ResultHelper:
						var formula = base.Formulas[d];
						dialogues.Add(string.Format(MsgResources.AC002, GetGapValue(formula)));
						break;
				}
			});

			return dialogues;
		}

		/// <summary>
		/// 找到填空項目并返回其值
		/// </summary>
		/// <param name="item">答題集合</param>
		/// <returns>填空項目的值</returns>
		private int GetGapValue(ArithmeticOperationsFormula item)
		{
			switch (item.Arithmetic.Gap)
			{
				case GapFilling.Answer:
					return item.Arithmetic.Answer;

				case GapFilling.Left:
					return item.Arithmetic.LeftParameter;

				case GapFilling.Right:
					return item.Arithmetic.RightParameter;
			}

			return 0;
		}
	}
}