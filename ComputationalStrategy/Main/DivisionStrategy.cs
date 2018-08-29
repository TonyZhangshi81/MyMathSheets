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
	public class DivisionStrategy : CalculatePatternBase
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

			_formula.RightParameter = GetLeftParameter(9);
			_formula.SignOfOperation = SignOfOperation.Division;
			_formula.LeftParameter = GetRightParameter(9, _formula.RightParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理(当被除数为0时,求解值可以为任何数)  只在随机除法填空题型且分子为0的情况下
			ResultSpecialHandling();
			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (_formula.Gap == GapFilling.Right && _formula.RightParameter == 0)
			{
				_formula.RightParameter = -999;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="rightParameter"></param>
		/// <returns></returns>
		protected override int GetRightParameter(int maximumLimit, int rightParameter)
		{
			var number = new RandomNumberComposition(0, maximumLimit);
			return number.GetRandomNumber() * rightParameter;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected override int GetLeftParameter(int maximumLimit)
		{
			var number = new RandomNumberComposition(1, maximumLimit);
			return number.GetRandomNumber();
		}
	}
}
