using MyMathSheets.CommonLib.Main.ArithmeticStrategy;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.Main.ArithmeticStrategy
{
	/// <summary>
	/// 
	/// </summary>
	[Calculate(SignOfOperation.Plus)]
	public class Adition : CalculateBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard, int minimumLimit = 0, GapFilling gap = GapFilling.Answer)
		{
			_formula = base.CreateFormula(maximumLimit, type, minimumLimit, gap);

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
		/// <param name="gap"></param>
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, Formula previousFormula, QuestionType type = QuestionType.GapFilling, int minimumLimit = 0, GapFilling gap = GapFilling.Right)
		{
			_formula = base.CreateFormula(maximumLimit, previousFormula, type, minimumLimit, gap);

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
			_formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(maximumLimit) : previousFormula.Answer;
			_formula.Sign = SignOfOperation.Plus;
			_formula.RightParameter = GetRightParameter(maximumLimit, _formula.LeftParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit">加法运算的最大值就是算式的答案Answer值</param>
		/// <param name="answer"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public override Formula CreateFormulaWithAnswer(int maximumLimit, int answer, QuestionType type = QuestionType.Standard, int minimumLimit = 0, GapFilling gap = GapFilling.Default)
		{
			_formula = base.CreateFormulaWithAnswer(maximumLimit, answer, type, minimumLimit, gap);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Plus;
			_formula.LeftParameter = GetLeftParameter(answer);
			_formula.RightParameter = _formula.Answer - _formula.LeftParameter;

			return _formula;
		}
	}
}
