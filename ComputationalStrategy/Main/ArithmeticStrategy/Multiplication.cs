using CommonLib.Util;
using ComputationalStrategy.Item;

namespace ComputationalStrategy.Main.ArithmeticStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class Multiplication : CalculatePatternBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard, int minimumLimit = 0)
		{
			_formula = base.CreateFormula(maximumLimit, type, minimumLimit);

			_formula.LeftParameter = GetLeftParameter(9);
			_formula.Sign = SignOfOperation.Multiple;
			_formula.RightParameter = GetRightParameter(9);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理（在乘法式中其中一个数值为零，那另一个值可以是任意一个数值）
			ResultSpecialHandling();

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit">乘法运算的最大值就是算式的答案Answer值</param>
		/// <param name="answer"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public override Formula CreateFormulaWithAnswer(int maximumLimit, int answer, QuestionType type = QuestionType.Standard, int minimumLimit = 0)
		{
			_formula = base.CreateFormulaWithAnswer(maximumLimit, answer, type, minimumLimit);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Multiple;
			_formula.LeftParameter = GetLeftParameter(9);
			// 判定是否能被整除
			if(_formula.Answer % _formula.LeftParameter != 0)
			{
				// 無解計算式（結果無法被整除）
				_formula.IsNoSolution = true;
				return _formula;
			}
			_formula.RightParameter = _formula.Answer / _formula.LeftParameter;

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (_formula.Gap == GapFilling.Left && _formula.RightParameter == 0)
			{
				_formula.LeftParameter = -999;
			}

			if (_formula.Gap == GapFilling.Right && _formula.LeftParameter == 0)
			{
				_formula.RightParameter = -999;
			}
		}
	}
}
