using ComputationalStrategy.Item;
using ComputationalStrategy.Main.ArithmeticStrategy;
using ComputationalStrategy.Main.Util;
using System.Collections.Generic;

namespace ComputationalStrategy.Main.Operation
{
	public class Arithmetic : SetThemeBase<List<Formula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public Arithmetic(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions)
			: this(fourOperationsType, new List<SignOfOperation>(), questionType, maximumLimit, numberOfQuestions)
		{
			_signs.Add(sign);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="signs"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public Arithmetic(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
			: base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
			_questionType = questionType;

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

			ICalculatePattern strategy = null;
			// 標準題型（指定單個運算符）
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = GetPatternInstance(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 計算式作成
					_formulas.Add(strategy.CreateFormula(_maximumLimit, _questionType));
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					RandomNumberComposition random = new RandomNumberComposition(0, _signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					SignOfOperation sign = _signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = GetPatternInstance(sign);
					// 計算式作成
					_formulas.Add(strategy.CreateFormula(_maximumLimit, _questionType));
				}
			}
		}
	}
}
