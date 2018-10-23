using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 等式大小比较
	/// </summary>
	[Operation(LayoutSetting.Preview.EqualityComparison)]
	public class EqualityComparison : OperationBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			EqualityComparisonParameter p = parameter as EqualityComparisonParameter;

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType);
					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType);

					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethod(leftFormula, rightFormula))
					{
						i--;
						continue;
					}

					// 計算式作成
					PushFormulas(p, leftFormula, rightFormula);
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 关系符左边计算式
					Formula leftFormula = GetFormulaForRandomNumber(p.Signs, p.MaximumLimit, p.QuestionType);
					// 关系符右边计算式
					Formula rightFormula = GetFormulaForRandomNumber(p.Signs, p.MaximumLimit, p.QuestionType);

					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethod(leftFormula, rightFormula))
					{
						i--;
						continue;
					}

					// 計算式作成
					PushFormulas(p, leftFormula, rightFormula);
				}
			}
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="p"></param>
		/// <param name="leftFormula">关系符左边计算式</param>
		/// <param name="rightFormula">关系符右边计算式</param>
		private void PushFormulas(EqualityComparisonParameter p, Formula leftFormula, Formula rightFormula)
		{
			// 計算式作成
			p.Formulas.Add(new EqualityFormula()
			{
				LeftFormula = leftFormula,
				RightFormula = rightFormula,
				Answer = leftFormula.Answer > rightFormula.Answer ? SignOfCompare.Greater :
								leftFormula.Answer < rightFormula.Answer ? SignOfCompare.Less : SignOfCompare.Equal
			});
		}

		/// <summary>
		/// 分别随机符号位作成左边关系式和右边关系式
		/// </summary>
		/// <returns>計算式</returns>
		private Formula GetFormulaForRandomNumber(IList<SignOfOperation> signs, int maximumLimit, QuestionType questionType)
		{
			RandomNumberComposition random = new RandomNumberComposition(0, signs.Count - 1);
			// 混合題型（加減乘除運算符實例隨機抽取）
			SignOfOperation sign = signs[random.GetRandomNumber()];
			// 對四則運算符實例進行cache管理
			ICalculate strategy = CalculateManager(sign);

			return strategy.CreateFormula(maximumLimit, questionType);
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：全零的情況
		/// </remarks>
		/// <param name="leftFormula">左邊算式</param>
		/// <param name="rightFormula">右邊算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(Formula leftFormula, Formula rightFormula)
		{
			// 全零的情況
			if (leftFormula.LeftParameter == 0 && leftFormula.RightParameter == 0 && leftFormula.Answer == 0)
			{
				return true;
			}
			// 全零的情況
			if (rightFormula.LeftParameter == 0 && rightFormula.RightParameter == 0 && rightFormula.Answer == 0)
			{
				return true;
			}
			return false;
		}
	}
}
