using ComputationalStrategy.Item;
using ComputationalStrategy.Main.ArithmeticStrategy;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using System.Collections.Generic;

namespace ComputationalStrategy.Main.Operation
{
	/// <summary>
	/// 等式大小比较
	/// </summary>
	public class EqualityComparison : SetThemeBase<List<EqualityFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public EqualityComparison(FourOperationsType fourOperationsType, SignOfOperation sign, int maximumLimit, int numberOfQuestions)
			: this(fourOperationsType, new List<SignOfOperation>(), maximumLimit, numberOfQuestions)
		{
			_signs.Add(sign);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="signs"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
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

			ICalculatePattern strategy = null;
			// 標準題型（指定單個運算符）
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = GetPatternInstance(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					var leftFormula = strategy.CreateFormula(_maximumLimit, _questionType);
					var rightFormula = strategy.CreateFormula(_maximumLimit, _questionType);

					// 計算式作成
					_formulas.Add(new EqualityFormula()
					{
						LeftFormula = leftFormula,
						RightFormula = rightFormula,
						Answer = leftFormula.Answer > rightFormula.Answer ? SignOfCompare.Greater :
									leftFormula.Answer < rightFormula.Answer ? SignOfCompare.Less : SignOfCompare.Equal
					});
				}
			}
		}

	}
}
