﻿using System;
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
	public class AditionStrategy : ICalculatePattern
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		public Formula Make(int maximumLimit)
		{
			var formula = new Formula();

			formula.LeftParameter = GetLeftParameter(maximumLimit);
			formula.SignOfOperation = Operation.add;
			formula.RightParameter = GetRightParameter(maximumLimit, formula.LeftParameter);
			formula.Answer = GetAnswer(formula.LeftParameter, formula.RightParameter);

			return formula;
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
			var number = new RandomNumberComposition(0, maximumLimit - leftParameter);
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
			return (leftParameter + rightParameter);
		}
	}
}
