using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Item;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Strategy
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
				Formula formula = strategy.CreateFormula(new CalculateParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = QuestionType.Default,
					MinimumLimit = 0
				});
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
		/// 情況2：有零的情況
		/// </remarks>
		/// <param name="p">題型參數</param>
		/// <param name="parameterA">第一個參數</param>
		/// <param name="parameterB">第二個參數</param>
		/// <param name="parameterC">第三個參數</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(CombinatorialEquationParameter p, int parameterA, int parameterB, int parameterC)
		{
			if(parameterA == 0 || parameterB == 0 || parameterC == 0)
			{
				return true;
			}

			// 情況1
			if (p.Formulas.ToList().Any(d =>
			{
				if ((d.ParameterA == parameterA || d.ParameterA == parameterB || d.ParameterA == parameterC)
					&& (d.ParameterB == parameterA || d.ParameterB == parameterB || d.ParameterB == parameterC)
					&& (d.ParameterC == parameterA || d.ParameterC == parameterB || d.ParameterC == parameterC))
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
