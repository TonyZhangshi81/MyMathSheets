using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Item;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Main.Parameters;
using System;

namespace MyMathSheets.ComputationalStrategy.EqualityComparison.Main.Strategy
{
	/// <summary>
	/// 等式大小比较
	/// </summary>
	[Operation("EqualityComparison")]
	public class EqualityComparison : OperationBase
	{
		/// <summary>
		/// 題型作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(EqualityComparisonParameter p, Func<SignOfOperation> signFunc)
		{
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 关系符左边计算式（指定單個運算符實例）
				Formula leftFormula = GetFormulaForRandomNumber(p.MaximumLimit, signFunc);
				// 关系符右边计算式（指定單個運算符實例）
				Formula rightFormula = GetFormulaForRandomNumber(p.MaximumLimit, signFunc);

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

		/// <summary>
		/// 題型作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			EqualityComparisonParameter p = parameter as EqualityComparisonParameter;

			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 按照指定數量作成相應的數學計算式（指定單個運算符實例）
				MarkFormulaList(p, () => { return p.Signs[0]; });
			}
			else
			{
				// 按照指定數量作成相應的數學計算式（加減乘除運算符實例隨機抽取）
				MarkFormulaList(p, () => { return p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]; });
			}
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
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
		/// <param name="maximumLimit">最大值</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>計算式</returns>
		private Formula GetFormulaForRandomNumber(int maximumLimit, Func<SignOfOperation> signFunc)
		{
			// 對四則運算符實例進行cache管理
			ICalculate strategy = CalculateManager(signFunc());

			return strategy.CreateFormula(new CalculateParameter()
			{
				MaximumLimit = maximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			});
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：有零的情況
		/// </remarks>
		/// <param name="leftFormula">左邊算式</param>
		/// <param name="rightFormula">右邊算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(Formula leftFormula, Formula rightFormula)
		{
			// 有零的情況
			if (leftFormula.LeftParameter == 0 || leftFormula.RightParameter == 0 || leftFormula.Answer == 0)
			{
				return true;
			}
			// 有零的情況
			if (rightFormula.LeftParameter == 0 || rightFormula.RightParameter == 0 || rightFormula.Answer == 0)
			{
				return true;
			}
			return false;
		}
	}
}