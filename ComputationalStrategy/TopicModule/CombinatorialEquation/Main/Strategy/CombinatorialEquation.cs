using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
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
	[Topic("CombinatorialEquation")]
	public class CombinatorialEquation : TopicBase
	{
		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter"></param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			CombinatorialEquationParameter p = parameter as CombinatorialEquationParameter;

			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 對四則運算符實例進行cache管理
				IArithmetic strategy = CalculateManager(p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]);
				// 計算式作成
				Formula formula = strategy.CreateFormula(new ArithmeticParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = QuestionType.Default,
					MinimumLimit = 1
				});
				// 獲得無效參數
				int invalidParameter = GetInvalidParameter(formula);

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
					ParameterD = invalidParameter
				});
				if (formula.Sign == SignOfOperation.Plus)
				{
					// 加法算式組合序列
					p.Formulas.Last().CombinatorialFormulas = new List<Formula>() {
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.RightParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Plus },
						new Formula(){ LeftParameter =  formula.RightParameter, RightParameter = formula.LeftParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Plus },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.LeftParameter, Answer = formula.RightParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.RightParameter, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction }
					};
				}
				else if (formula.Sign == SignOfOperation.Subtraction)
				{
					// 減法算式組合序列
					p.Formulas.Last().CombinatorialFormulas = new List<Formula>() {
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.RightParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction },
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.Answer, Answer = formula.RightParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Subtraction },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.RightParameter, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Plus },
						new Formula(){ LeftParameter =  formula.RightParameter, RightParameter = formula.Answer, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Plus }
					};
				}
				else if (formula.Sign == SignOfOperation.Multiple)
				{
					// 乘法算式組合序列
					p.Formulas.Last().CombinatorialFormulas = new List<Formula>() {
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.RightParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Multiple },
						new Formula(){ LeftParameter =  formula.RightParameter, RightParameter = formula.LeftParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Multiple },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.LeftParameter, Answer = formula.RightParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Division },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.RightParameter, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Division }
					};
				}
				else
				{
					// 除法算式組合序列
					p.Formulas.Last().CombinatorialFormulas = new List<Formula>() {
						new Formula(){ LeftParameter =  formula.RightParameter, RightParameter = formula.Answer, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Multiple },
						new Formula(){ LeftParameter =  formula.Answer, RightParameter = formula.RightParameter, Answer = formula.LeftParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Multiple },
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.RightParameter, Answer = formula.Answer, Gap = GapFilling.Default, Sign = SignOfOperation.Division },
						new Formula(){ LeftParameter =  formula.LeftParameter, RightParameter = formula.Answer, Answer = formula.RightParameter, Gap = GapFilling.Default, Sign = SignOfOperation.Division }
					};
				}

				// 將參數亂序排列
				DisorganizeParameter(p.Formulas.Last());
			}
		}

		/// <summary>
		/// 將參數亂序排列
		/// </summary>
		/// <param name="formula">計算式</param>
		private void DisorganizeParameter(CombinatorialFormula formula)
		{
			// 參數隊列
			List<int> parameters = new List<int>() { formula.ParameterA, formula.ParameterB, formula.ParameterC, formula.ParameterD };
			// 亂序處理
			for (int index = parameters.Count - 1; index > 0; index--)
			{
				int random = CommonUtil.GetRandomNumber(0, index);
				int tmp = parameters[random];
				parameters[random] = parameters[index];
				parameters[index] = tmp;
			}

			formula.ParameterA = parameters[0];
			formula.ParameterB = parameters[1];
			formula.ParameterC = parameters[2];
			formula.ParameterD = parameters[3];
		}

		/// <summary>
		/// 設定無效參數
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns>無效參數</returns>
		private int GetInvalidParameter(Formula formula)
		{
			int invalidParameter = 0;
			bool isFind = false;
			while (!isFind)
			{
				// 隨機數中選取無效參數
				var parameter = CommonUtil.GetRandomNumber(1, 9);
				// 無效參數不能出現在計算式中
				if (parameter != formula.LeftParameter && parameter != formula.RightParameter && parameter != formula.Answer
					&& IsInvalidParameter(formula, parameter))
				{
					isFind = true;
					// 參數設定
					invalidParameter = parameter;
				}
			}
			return invalidParameter;
		}

		/// <summary>
		/// 檢查是否能夠與現有等式組成新的等式
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <param name="parameter">無效參數</param>
		/// <returns>不能組成：true</returns>
		private bool IsInvalidParameter(Formula formula, int parameter)
		{
			if (formula.Sign == SignOfOperation.Plus && parameter > formula.Answer
				&& ((formula.LeftParameter + formula.Answer) == parameter || (formula.RightParameter + formula.Answer) == parameter))
			{
				// 加法且無效值大於最大值(eg: 2,3,5,[7]  7->不可使用)
				return false;
			}
			else if (formula.Sign == SignOfOperation.Plus && parameter < formula.Answer
			   && ((formula.LeftParameter + parameter) == formula.RightParameter || (formula.RightParameter + parameter) == formula.LeftParameter))
			{
				// 加法且無效值大於最大值(eg: 1,[2],3,4  2->不可使用)
				return false;
			}
			else if (formula.Sign == SignOfOperation.Subtraction && parameter > formula.LeftParameter
				&& ((formula.LeftParameter + formula.Answer) == parameter || (formula.LeftParameter + formula.RightParameter) == parameter))
			{
				// 減法且無效值大於最大值(eg: 2,3,5,[7]  7->不可使用)
				return false;
			}
			else if (formula.Sign == SignOfOperation.Subtraction && parameter < formula.Answer
			   && ((formula.RightParameter + parameter) == formula.Answer || (formula.Answer + parameter) == formula.RightParameter))
			{
				// 減法且無效值大於最大值(eg: 1,[2],3,4  2->不可使用)
				return false;
			}

			return true;
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：题型集合中三個參數不能同时存在
		/// 情況2：有零的情況
		/// 情况3：三个参数不能有相同的数字
		/// </remarks>
		/// <param name="p">題型參數</param>
		/// <param name="parameterA">第一個參數</param>
		/// <param name="parameterB">第二個參數</param>
		/// <param name="parameterC">第三個參數</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(CombinatorialEquationParameter p, int parameterA, int parameterB, int parameterC)
		{
			// 情況2
			if (parameterA == 0 || parameterB == 0 || parameterC == 0)
			{
				return true;
			}

			// 情況3
			if (parameterA == parameterB || parameterB == parameterC || parameterA == parameterC)
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