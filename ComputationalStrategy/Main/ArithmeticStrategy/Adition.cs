using CommonLib.Util;
using ComputationalStrategy.Item;

namespace ComputationalStrategy.Main.ArithmeticStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class Adition : CalculatePatternBase
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

			_formula.LeftParameter = GetLeftParameter(maximumLimit);
			_formula.Sign = SignOfOperation.Plus;
			_formula.RightParameter = GetRightParameter(maximumLimit, _formula.LeftParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}

		/// <summary>
		/// 该构造用于计算接龙题型(即:计算式左边值等于上一个计算式的结果值)
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="previousFormula">前一层计算式</param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, Formula previousFormula, QuestionType type = QuestionType.GapFilling, int minimumLimit = 0)
		{
			_formula = base.CreateFormula(maximumLimit, previousFormula, type, minimumLimit);

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
			_formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(maximumLimit) : previousFormula.Answer;
			_formula.Sign = SignOfOperation.Plus;
			_formula.RightParameter = GetRightParameter(maximumLimit, _formula.LeftParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}
	}
}
