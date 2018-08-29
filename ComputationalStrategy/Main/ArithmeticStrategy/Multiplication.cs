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
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard)
		{
			_formula = base.CreateFormula(maximumLimit, type);

			_formula.LeftParameter = GetLeftParameter(9);
			_formula.SignOfOperation = SignOfOperation.Multiple;
			_formula.RightParameter = GetRightParameter(9);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理
			ResultSpecialHandling();

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
