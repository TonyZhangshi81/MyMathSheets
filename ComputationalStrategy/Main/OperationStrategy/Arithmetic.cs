using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 四則遠算題
	/// </summary>
	[Operation(LayoutSetting.Preview.Arithmetic)]
	public class Arithmetic : OperationBase<List<Formula>>
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="questionType">题型（标准、随机填空）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public Arithmetic(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
			: base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
			_questionType = questionType;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			ArithmeticParameter p = parameter as ArithmeticParameter;

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
					Formula formula = strategy.CreateFormula(_maximumLimit, _questionType);
					if (CheckIsNeedInverseMethod(_formulas, formula))
					{
						i--;
						continue;
					}
					// 計算式作成
					_formulas.Add(formula);
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
					strategy = CalculateManager(sign);

					var formula = strategy.CreateFormula(_maximumLimit, _questionType);
					if (CheckIsNeedInverseMethod(_formulas, formula))
					{
						i--;
						continue;
					}

					// 計算式作成
					_formulas.Add(formula);
				}
			}

			p.Formulas = _formulas;
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
		private bool CheckIsNeedInverseMethod(List<Formula> preFormulas, Formula currentFormula)
		{
			// 全零的情況
			if(currentFormula.LeftParameter == 0 && currentFormula.RightParameter == 0 && currentFormula.Answer == 0)
			{
				return true;
			}
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.LeftParameter == currentFormula.LeftParameter
			&& d.RightParameter == currentFormula.RightParameter
			&& d.Answer == currentFormula.Answer
			&& d.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}
	}
}
