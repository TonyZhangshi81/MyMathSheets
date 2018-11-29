using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
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
		/// <param name="parameter"></param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			ArithmeticParameter p = parameter as ArithmeticParameter;

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
					p.Formulas.Add(new ArithmeticFormula
					{
						// 四則運算式
						Arithmetic = formula,
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
					RandomNumberComposition random = new RandomNumberComposition(0, p.Signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					SignOfOperation sign = p.Signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager(sign);

					var formula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType);
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
				RandomNumberComposition random = new RandomNumberComposition(0, (int)LeftOrRight.Right);
				return ((int)random.GetRandomNumber() == 1);
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
