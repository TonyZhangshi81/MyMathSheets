using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Item;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Helper;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Strategy
{
	/// <summary>
	/// 比多少題
	/// </summary>
	[Operation("HowMuchMore")]
	public class HowMuchMore : TopicBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			HowMuchMoreParameter p = parameter as HowMuchMoreParameter;

			// 指定單個運算符實例（此題只能用減法運算符）
			IArithmetic strategy = CalculateManager(SignOfOperation.Subtraction);

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 計算式作成
				Formula formula = strategy.CreateFormula(new ArithmeticParameter()
				{
					// 10以內計算
					MaximumLimit = (p.MaximumLimit > 10) ? 10 : p.MaximumLimit,
					QuestionType = QuestionType.Default,
					MinimumLimit = 0
				});

				if (CheckIsNeedInverseMethod(p.Formulas, formula))
				{
					i--;
					continue;
				}

				// 運算式作成
				HowMuchMoreFormula howMuchMoreFormula = new HowMuchMoreFormula()
				{
					DefaultFormula = formula,
					// 選擇左邊作為題目的條件還是右邊（隨機獲取）
					DisplayLeft = CommonUtil.GetRandomNumber(LeftOrRight.Left, LeftOrRight.Right) == LeftOrRight.Left,
					// 題型選擇：是表示多的題還是少的題（隨機獲取）
					ChooseMore = CommonUtil.GetRandomNumber(MoreOrLess.More, MoreOrLess.Less) == MoreOrLess.More
				};
				// 文字表述部分內容作成
				howMuchMoreFormula.MathWordProblem = CreateMathWordProblem(howMuchMoreFormula);
				// 答案值（如果顯示的是左邊值，那麼答案就是右邊的值）
				howMuchMoreFormula.Answer = ((howMuchMoreFormula.DisplayLeft) ? howMuchMoreFormula.DefaultFormula.RightParameter : howMuchMoreFormula.DefaultFormula.LeftParameter);

				// 計算式作成
				p.Formulas.Add(howMuchMoreFormula);
			}

			// 智能提示作成
			VirtualHelperBase<HowMuchMoreFormula> helper = new HowMuchMoreDialogue();
			p.BrainpowerHint = helper.CreateHelperDialogue(p.Formulas);
		}

		/// <summary>
		/// 比較多少的文字表述部分內容
		/// </summary>
		/// <param name="formula">當前算式</param>
		/// <returns>文字表述部分內容</returns>
		private string CreateMathWordProblem(HowMuchMoreFormula formula)
		{
			StringBuilder content = new StringBuilder();
			if (formula.ChooseMore)
			{
				content.AppendFormat("{0}比{1}多{2}個", "Left", "Right", formula.DefaultFormula.Answer);
			}
			else
			{
				content.AppendFormat("{0}比{1}少{2}個", "Right", "Left", formula.DefaultFormula.Answer);
			}

			return content.ToString();
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：全零的情況
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<HowMuchMoreFormula> preFormulas, Formula currentFormula)
		{
			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.DefaultFormula.LeftParameter == currentFormula.LeftParameter
				&& d.DefaultFormula.RightParameter == currentFormula.RightParameter
				&& d.DefaultFormula.Answer == currentFormula.Answer
				&& d.DefaultFormula.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}
	}
}