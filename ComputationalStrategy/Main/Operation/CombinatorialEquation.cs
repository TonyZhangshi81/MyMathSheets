using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.ArithmeticStrategy;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.Operation
{
	/// <summary>
	/// 組合計算式
	/// </summary>
	public class CombinatorialEquation : SetThemeBase<List<CombinatorialFormula>>
	{
		/// <summary>
		/// 組合計算式題型構築對象初期化
		/// </summary>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public CombinatorialEquation(int maximumLimit, int numberOfQuestions)
			: base(maximumLimit, numberOfQuestions)
		{
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		public override void MarkFormulaList()
		{
			ICalculatePattern strategy = null;
			for (var i = 0; i < _numberOfQuestions; i++)
			{
				// 對四則運算符實例進行cache管理
				strategy = GetPatternInstance(SignOfOperation.Plus);
				// 計算式作成
				Formula formula = strategy.CreateFormula(_maximumLimit, QuestionType.Standard);
				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(formula.LeftParameter, formula.RightParameter, formula.Answer))
				{
					i--;
					continue;
				}

				_formulas.Add(new CombinatorialFormula()
				{
					ParameterA = formula.LeftParameter,
					ParameterB = formula.Answer,
					ParameterC = formula.RightParameter,
					CombinatorialFormulas = new List<Formula>() {
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.RightParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Plus },
						new Formula(){ LeftParameter =  formula.RightParameter, RightParameter = formula.LeftParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Plus },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.LeftParameter, Answer = formula.RightParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.RightParameter, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction }
					}
				});
			}
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：三個參數存在一致
		/// </remarks>
		/// <param name="parameterA">第一個參數</param>
		/// <param name="parameterB">第二個參數</param>
		/// <param name="parameterC">第三個參數</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(int parameterA, int parameterB, int parameterC)
		{
			// 情況1
			if (_formulas.ToList().Any(d =>
			{
				int[] ary = new int[3] { d.ParameterA, d.ParameterB, d.ParameterC };
				int pAIndex = ary.ToList().IndexOf(parameterA);
				int pBIndex = ary.ToList().IndexOf(parameterB);
				int pCIndex = ary.ToList().IndexOf(parameterC);
				if (pAIndex == pBIndex && pBIndex == pCIndex)
				{
					return true;
				}
				return false;
			}))
			{
				return true;
			}
			return false;
		}
	}
}
