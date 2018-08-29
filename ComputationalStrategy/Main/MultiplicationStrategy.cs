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
	public class MultiplicationStrategy : ICalculatePattern
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public Formula Make(int maximumLimit, QuestionType type = QuestionType.Standard)
		{
			var formula = new Formula();

			formula.LeftParameter = GetLeftParameter(10);
			formula.SignOfOperation = SignOfOperation.Multiple;
			formula.RightParameter = GetRightParameter(10, 1);
			formula.Answer = GetAnswer(formula.LeftParameter, formula.RightParameter);
			formula.Gap = GapFilling.Answer;

			if (type == QuestionType.GapFilling)
			{
				formula.Gap = GetGapItem();
			}

			return formula;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private GapFilling GetGapItem()
		{
			var number = new RandomNumberComposition(0, 2);
			return (GapFilling)number.GetRandomNumber();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		private int GetLeftParameter(int maximumLimit)
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
		private int GetRightParameter(int maximumLimit, int leftParameter)
		{
			var number = new RandomNumberComposition(0, maximumLimit / ((leftParameter == 0) ? 1 : leftParameter));
			return number.GetRandomNumber();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="leftParameter"></param>
		/// <param name="rightParameter"></param>
		/// <returns></returns>
		public int GetAnswer(int leftParameter, int rightParameter)
		{
			return (leftParameter * rightParameter);
		}
	}
}
