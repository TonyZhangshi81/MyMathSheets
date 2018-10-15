using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 比多少題
	/// </summary>
	[Operation(LayoutSetting.Preview.HowMuchMore)]
	public class HowMuchMore : OperationBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			HowMuchMoreParameter p = parameter as HowMuchMoreParameter;

			ICalculate strategy = null;
			// 標準題型（指定單個運算符 -> 此題只能用減法運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				if (p.Signs[0] != SignOfOperation.Subtraction)
				{
					return;
				}

				RandomNumberComposition random;
				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					Formula formula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType);
					if (CheckIsNeedInverseMethod(p.Formulas, formula))
					{
						i--;
						continue;
					}

					// 在多少一方中隨機選擇一個用於顯示默認出現的圖形
					random = new RandomNumberComposition(0, (int)HowMuchMoreType.Right);

					// 運算式作成
					HowMuchMoreFormula defaultFormula = new HowMuchMoreFormula()
					{
						DefaultFormula = formula,
						// 被選擇顯示的一方
						LeftOrRightParameter = (HowMuchMoreType)random.GetRandomNumber(),
						// 文字表述部分內容作成
						MathWordProblem = CreateMathWordProblem(formula.Answer)
					};
					defaultFormula.Answer = (defaultFormula.LeftOrRightParameter == HowMuchMoreType.Left) ? defaultFormula.DefaultFormula.RightParameter : defaultFormula.DefaultFormula.LeftParameter;

					// 計算式作成
					p.Formulas.Add(defaultFormula);
				}
			}
		}

		/// <summary>
		/// 比較多少的文字表述部分內容
		/// </summary>
		/// <param name="answer">兩個值之間的差額</param>
		/// <returns>文字表述部分內容</returns>
		private string CreateMathWordProblem(int answer)
		{
			// 選多還是少（左邊多；右邊少；）
			RandomNumberComposition random = new RandomNumberComposition(0, (int)HowMuchMoreType.Right);
			var param = (HowMuchMoreType)random.GetRandomNumber();

			StringBuilder content = new StringBuilder();
			if(param == HowMuchMoreType.Left)
			{
				content.AppendFormat("{0}比{1}多{2}個", "Left", "Right", answer);
			}
			else if (param == HowMuchMoreType.Right)
			{
				content.AppendFormat("{0}比{1}少{2}個", "Right", "Left", answer);
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
