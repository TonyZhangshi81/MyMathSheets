using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalStrategy.Item;

namespace ComputationalStrategy.Main
{
	/// <summary>
	/// 
	/// </summary>
	public class SubtractionStrategy : CalculatePatternBase
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

			_formula.LeftParameter = GetLeftParameter(maximumLimit);
			_formula.SignOfOperation = SignOfOperation.Subtraction;
			_formula.RightParameter = GetRightParameter(_formula.LeftParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}
	}
}
