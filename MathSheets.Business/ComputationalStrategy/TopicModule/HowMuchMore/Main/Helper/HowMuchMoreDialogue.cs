using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Item;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Properties;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Helper
{
	/// <summary>
	/// 比多少題型的智能提示作成類
	/// </summary>
	public class HowMuchMoreDialogue : VirtualHelperBase<HowMuchMoreFormula>
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
						dialogues.Add(MsgResources.HMM003);
						break;

					case DialogueType.ResultHelper:
						dialogues.Add(GetDialogue(base.Formulas[d]));
						break;
				}
			});

			return dialogues;
		}

		/// <summary>
		/// 根據答題集合數量配置智能提示所需參數
		/// </summary>
		/// <remarks>
		/// 智能提示最大條數: 每2題有一個提示
		/// </remarks>
		protected override int SettingParameterInit()
		{
			// 智能提示最大條數
			return Convert.ToInt32(Math.Ceiling(Formulas.Count / 2.0m));
		}

		/// <summary>
		/// 取得提示語句
		/// </summary>
		/// <param name="formula">答題集合</param>
		/// <returns>提示語句</returns>
		private string GetDialogue(HowMuchMoreFormula formula)
		{
			if (formula.ChooseMore)
			{
				// 求多的提示
				return string.Format(MsgResources.HMM001, formula.DefaultFormula.RightParameter, formula.DefaultFormula.Answer);
			}
			else
			{
				// 求少的提示
				return string.Format(MsgResources.HMM002, formula.DefaultFormula.LeftParameter, formula.DefaultFormula.Answer);
			}
		}
	}
}