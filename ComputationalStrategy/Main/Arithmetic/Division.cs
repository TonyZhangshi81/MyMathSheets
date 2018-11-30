using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 
	/// </summary>
	[Calculate(SignOfOperation.Division)]
	public class Division : CalculateBase
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

			_formula.RightParameter = GetLeftParameter(9);
			_formula.Sign = SignOfOperation.Division;
			_formula.LeftParameter = GetRightParameter(9, _formula.RightParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理(当被除数为0时,求解值可以为任何数)  只在随机除法填空题型且分子为0的情况下
			ResultSpecialHandling();
			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="previousFormula"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public override Formula CreateFormula(int maximumLimit, Formula previousFormula, QuestionType type = QuestionType.GapFilling, int minimumLimit = 0, GapFilling gap = GapFilling.Right)
		{
			_formula = base.CreateFormula(maximumLimit, previousFormula, type, minimumLimit, gap);

			// TODO

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="answer"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public override Formula CreateFormulaWithAnswer(int maximumLimit, int answer, QuestionType type = QuestionType.Standard, int minimumLimit = 0, GapFilling gap = GapFilling.Default)
		{
			_formula = base.CreateFormulaWithAnswer(maximumLimit, answer, type, minimumLimit, gap);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Division;
			_formula.LeftParameter = GetLeftParameter(9);
			// 判定是否超出九九乘法口訣上限值
			if (_formula.Answer > 81)
			{
				// 無解計算式（結果無法被整除）
				_formula.IsNoSolution = true;
				return _formula;
			}
			_formula.RightParameter = _formula.Answer * _formula.LeftParameter;

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
			var number = CommonUtil.GetRandomNumber(_minimumLimit, maximumLimit);
			return number * rightParameter;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected override int GetLeftParameter(int maximumLimit)
		{
			var number = CommonUtil.GetRandomNumber(1, maximumLimit);
			return number;
		}
	}
}
