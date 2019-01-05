using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Strategy
{
	/// <summary>
	/// 四則遠算題
	/// </summary>
	[Operation(LayoutSetting.Preview.Arithmetic)]
	public class Arithmetic : OperationBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(ArithmeticParameter p, Func<SignOfOperation> signFunc)
		{
			ICalculate strategy = null;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				strategy = CalculateManager(signFunc());
				Formula formula = strategy.CreateFormula(new CalculateParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = p.QuestionType,
					MinimumLimit = 0
				});
				if (CheckIsNeedInverseMethod(p.Formulas, formula))
				{
					i--;
					continue;
				}
				// 計算式作成
				p.Formulas.Add(new ArithmeticFormula
				{
					// 四則運算式
					Arithmetic = formula,
					// 多級運算式
					MultistageArithmetic = GetMultistageFormula(p, formula, () => { return signFunc(); }),
					// 等式值是不是出現在右邊
					AnswerIsRight = IsRight
				});
			}
		}

		/// <summary>
		/// 多級運算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="pervFormula">第一級計算式</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>四則運算式</returns>
		private Formula GetMultistageFormula(ArithmeticParameter p, Formula pervFormula, Func<SignOfOperation> signFunc)
		{
			// 第一級計算式右加數值
			if (pervFormula.RightParameter == 0)
			{
				return null;
			}

			ICalculate strategy = CalculateManager(signFunc());
			// 計算式作成（依據左邊算式的答案推算右邊的算式）
			Formula formula = strategy.CreateFormulaWithAnswer(new CalculateParameter()
			{
				MaximumLimit = p.MaximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			}, pervFormula.RightParameter);

			if(pervFormula.Gap == GapFilling.Right)
			{
				formula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);
			}

			return formula;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			ArithmeticParameter p = parameter as ArithmeticParameter;

			// 標準題型
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 算式作成（指定單個運算符實例）
				MarkFormulaList(p, () => { return p.Signs[0]; });
			}
			else
			{
				// 算式作成（加減乘除運算符實例隨機抽取）
				MarkFormulaList(p, () => { return p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]; });
			}
		}

		/// <summary>
		/// 等式值是不是出現在左邊
		/// </summary>
		public bool IsRight => CommonUtil.GetRandomNumber(LeftOrRight.Left, LeftOrRight.Right) == LeftOrRight.Right;

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
		private bool CheckIsNeedInverseMethod(IList<ArithmeticFormula> preFormulas, Formula currentFormula)
		{
			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.Arithmetic.LeftParameter == currentFormula.LeftParameter
				&& d.Arithmetic.RightParameter == currentFormula.RightParameter
				&& d.Arithmetic.Answer == currentFormula.Answer
				&& d.Arithmetic.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}
	}
}
