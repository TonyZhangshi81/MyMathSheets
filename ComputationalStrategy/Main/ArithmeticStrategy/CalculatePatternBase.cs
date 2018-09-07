using CommonLib.Util;
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
		/// 随机下限值取得（默认值为0）
		/// </summary>
		protected int _minimumLimit { get; set; }
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
			var number = new RandomNumberComposition(_minimumLimit, maximumLimit);
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
			var number = new RandomNumberComposition(_minimumLimit, maximumLimit - leftParameter);
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
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public virtual Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard, int minimumLimit = 0)
		{
			_formula = new Formula
			{
				// 默認情況為無填空項
				Gap = GapFilling.Default
			};

			// 随机下限值
			_minimumLimit = minimumLimit;
			// 默認填空項是答案項
			_formula.Gap = GapFilling.Answer;
			if (type == QuestionType.GapFilling)
			{
				// 要求隨機設定填空項
				_formula.Gap = GapFillingItem;
			}

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="previousFormula"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public virtual Formula CreateFormula(int maximumLimit, Formula previousFormula, QuestionType type = QuestionType.GapFilling, int minimumLimit = 0)
		{
			_formula = new Formula
			{
				// 默認情況為無填空項
				Gap = GapFilling.Default
			};

			// 随机下限值
			_minimumLimit = minimumLimit;

			// 设定填空项位置
			_formula.Gap = GapFilling.Right;

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="answer"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <returns></returns>
		public virtual Formula CreateFormulaWithAnswer(int maximumLimit, int answer, QuestionType type = QuestionType.Standard, int minimumLimit = 0)
		{
			_formula = new Formula
			{
				// 默認情況為無填空項
				Gap = GapFilling.Default
			};

			// 随机下限值
			_minimumLimit = minimumLimit;

			return _formula;
		}
	}
}
