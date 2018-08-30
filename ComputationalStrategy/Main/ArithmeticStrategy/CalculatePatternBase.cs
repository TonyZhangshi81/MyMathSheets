using ComputationalStrategy.Item;
using ComputationalStrategy.Main.Util;
using System;

namespace ComputationalStrategy.Main.ArithmeticStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class CalculatePatternBase : ICalculatePattern
	{
		/// <summary>
		/// 
		/// </summary>
		protected Formula _formula { get; set; }

		/// <summary>
		/// 對在等式中的三個數值隨機產生填空項（用於填空題型）
		/// </summary>
		/// <returns></returns>
		protected virtual GapFilling GapFillingItem
		{
			get
			{
				RandomNumberComposition number = new RandomNumberComposition(0, (int)GapFilling.Answer);
				return (GapFilling)number.GetRandomNumber();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected virtual int GetLeftParameter(int maximumLimit)
		{
			var number = new RandomNumberComposition(0, maximumLimit);
			return number.GetRandomNumber();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="leftParameter"></param>
		/// <returns></returns>
		protected virtual int GetRightParameter(int maximumLimit, int leftParameter = 0)
		{
			var number = new RandomNumberComposition(0, maximumLimit - leftParameter);
			return number.GetRandomNumber();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="leftParameter"></param>
		/// <param name="rightParameter"></param>
		/// <returns></returns>
		protected virtual int GetAnswer(int leftParameter, int rightParameter)
		{
			if (_formula == null)
			{
				throw new NullReferenceException();
			}

			switch (_formula.Sign)
			{
				case SignOfOperation.Plus:
					return (leftParameter + rightParameter);
				case SignOfOperation.Subtraction:
					return (leftParameter - rightParameter);
				case SignOfOperation.Multiple:
					return (leftParameter * rightParameter);
				case SignOfOperation.Division:
					return (leftParameter / rightParameter);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public virtual Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard)
		{
			_formula = new Formula();

			if (type == QuestionType.GapFilling)
			{
				_formula.Gap = GapFillingItem;
			}
			return _formula;
		}
	}
}
