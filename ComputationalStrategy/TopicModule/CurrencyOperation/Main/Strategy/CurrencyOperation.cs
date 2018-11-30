using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyOperation.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Strategy
{
	/// <summary>
	/// 貨幣運算題型
	/// </summary>
	[Operation(LayoutSetting.Preview.CurrencyOperation)]
	public class CurrencyOperation : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			CurrencyOperationParameter p = parameter as CurrencyOperationParameter;

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
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
					// 計算式作成
					p.Formulas.Add(new CurrencyOperationFormula
					{
						// 貨幣運算方程式
						CurrencyArithmetic = formula,
						// 等式值是不是出現在右邊
						AnswerIsRight = IsRight
					});
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 對運算符實例進行cache管理
					strategy = CalculateManager((SignOfOperation)CommonUtil.GetRandomNumber((int)SignOfOperation.Plus, (int)SignOfOperation.Subtraction));

					var formula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType);
					if (CheckIsNeedInverseMethod(p.Formulas, formula))
					{
						i--;
						continue;
					}

					// 計算式作成
					p.Formulas.Add(new CurrencyOperationFormula
					{
						// 貨幣運算方程式
						CurrencyArithmetic = formula,
						// 等式值是不是出現在右邊
						AnswerIsRight = IsRight
					});
				}
			}
		}

		/// <summary>
		/// 等式值是不是出現在左邊
		/// </summary>
		public bool IsRight
		{
			get
			{
				return ((LeftOrRight)CommonUtil.GetRandomNumber(0, (int)LeftOrRight.Right) == LeftOrRight.Right);
			}
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
		private bool CheckIsNeedInverseMethod(IList<CurrencyOperationFormula> preFormulas, Formula currentFormula)
		{
			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.CurrencyArithmetic.LeftParameter == currentFormula.LeftParameter
			&& d.CurrencyArithmetic.RightParameter == currentFormula.RightParameter
			&& d.CurrencyArithmetic.Answer == currentFormula.Answer
			&& d.CurrencyArithmetic.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}

	}
}
