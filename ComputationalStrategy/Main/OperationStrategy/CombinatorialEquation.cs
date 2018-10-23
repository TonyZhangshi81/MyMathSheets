using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 組合計算式
	/// </summary>
	[Operation(LayoutSetting.Preview.CombinatorialEquation)]
	public class CombinatorialEquation : OperationBase
	{
		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter"></param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			CombinatorialEquationParameter p = parameter as CombinatorialEquationParameter;

			ICalculate strategy = null;
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 對四則運算符實例進行cache管理
				strategy = CalculateManager(SignOfOperation.Plus);
				// 計算式作成
				Formula formula = strategy.CreateFormula(p.MaximumLimit, QuestionType.Standard);
				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p, formula.LeftParameter, formula.RightParameter, formula.Answer))
				{
					i--;
					continue;
				}

				p.Formulas.Add(new CombinatorialFormula()
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
		/// <param name="p"></param>
		/// <param name="parameterA">第一個參數</param>
		/// <param name="parameterB">第二個參數</param>
		/// <param name="parameterC">第三個參數</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(CombinatorialEquationParameter p, int parameterA, int parameterB, int parameterC)
		{
			// 情況1
			if (p.Formulas.ToList().Any(d =>
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
