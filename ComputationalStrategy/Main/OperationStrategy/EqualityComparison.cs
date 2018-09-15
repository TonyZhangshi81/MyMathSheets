using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 等式大小比较
	/// </summary>
	[Operation(LayoutSetting.Preview.EqualityComparison)]
	public class EqualityComparison : OperationBase<List<EqualityFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public EqualityComparison(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, int maximumLimit, int numberOfQuestions) :
			base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void MarkFormulaList()
		{
			if (_fourOperationsType == FourOperationsType.Default)
			{
				return;
			}

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = CalculateManager(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(_maximumLimit, _questionType);
					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormula(_maximumLimit, _questionType);
					// 計算式作成
					PushFormulas(leftFormula, rightFormula);
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 关系符左边计算式
					Formula leftFormula = GetFormulaForRandomNumber();
					// 关系符右边计算式
					Formula rightFormula = GetFormulaForRandomNumber();
					// 計算式作成
					PushFormulas(leftFormula, rightFormula);
				}
			}
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="leftFormula">关系符左边计算式</param>
		/// <param name="rightFormula">关系符右边计算式</param>
		private void PushFormulas(Formula leftFormula, Formula rightFormula)
		{
			// 計算式作成
			_formulas.Add(new EqualityFormula()
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
		private Formula GetFormulaForRandomNumber()
		{
			RandomNumberComposition random = new RandomNumberComposition(0, _signs.Count - 1);
			// 混合題型（加減乘除運算符實例隨機抽取）
			SignOfOperation sign = _signs[random.GetRandomNumber()];
			// 對四則運算符實例進行cache管理
			ICalculate strategy = CalculateManager(sign);

			return strategy.CreateFormula(_maximumLimit, _questionType);
		}
	}
}
