using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	[Operation(LayoutSetting.Preview.ComputingConnection)]
	public class ComputingConnection : OperationBase<List<ConnectionFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public ComputingConnection(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, int maximumLimit, int numberOfQuestions) :
			base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
		}

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
				// 考虑乘除法接龙答题结果会超出限制,所以只随机加减法策略
				if (_signs[0] == SignOfOperation.Division || _signs[0] == SignOfOperation.Multiple)
				{
					return;
				}

				// 指定單個運算符實例
				strategy = CalculateManager(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					Formula previousFormula = null;
					IList<Formula> formulas = new List<Formula>();
					// 随机获取接龙层数
					int sectionNumber = GetSectionNumber();
					for (var j = 0; j < sectionNumber; j++)
					{
						Formula formula = strategy.CreateFormula(_maximumLimit, previousFormula, _questionType);
						previousFormula = formula;

						formulas.Add(formula);
					}
					_formulas.Add(new ConnectionFormula() { ConfixFormulas = formulas, ConfixNumber = formulas.Count });
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					Formula previousFormula = null;
					IList<Formula> formulas = new List<Formula>();

					// 随机获取接龙层数
					int sectionNumber = GetSectionNumber();
					for (var j = 0; j < sectionNumber; j++)
					{
						RandomNumberComposition random = new RandomNumberComposition(0, (int)SignOfOperation.Subtraction);
						// 混合題型（加減運算符實例隨機抽取） 注意:考虑乘除法接龙答题结果会超出限制,所以只随机加减法策略
						SignOfOperation sign = _signs[random.GetRandomNumber()];
						// 對四則運算符實例進行cache管理
						strategy = CalculateManager(sign);

						Formula formula = strategy.CreateFormula(_maximumLimit, previousFormula, _questionType);
						previousFormula = formula;

						formulas.Add(formula);
					}
					_formulas.Add(new ConnectionFormula() { ConfixFormulas = formulas, ConfixNumber = formulas.Count });
				}
			}
		}

		/// <summary>
		/// 连等式总节数(预设3~5随机抽取)
		/// </summary>
		/// <returns></returns>
		private int GetSectionNumber()
		{
			RandomNumberComposition number = new RandomNumberComposition(3, 5);
			return number.GetRandomNumber();
		}
	}
}
