﻿using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class OperationBase : IOperation
	{
		/// <summary>
		/// 
		/// </summary>
		private CalculateHelper _helper;

		/// <summary>
		/// 
		/// </summary>
		protected CalculateHelper Helper => _helper ?? (_helper = new CalculateHelper());

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		protected ICalculate CalculateManager(SignOfOperation sign)
		{
			return Helper.CreateCalculateInstance(sign);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public abstract void MarkFormulaList(ParameterBase parameter);
	}
}