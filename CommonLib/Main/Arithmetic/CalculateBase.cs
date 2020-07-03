using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	///
	/// </summary>
	public abstract class CalculateBase : ICalculate
	{
		/// <summary>
		/// 随机下限值取得（默认值为0）
		/// </summary>
		protected int MinimumLimit { get; set; }

		/// <summary>
		///
		/// </summary>
		protected Formula Formula { get; set; }

		/// <summary>
		///
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected virtual int GetLeftParameter(int maximumLimit)
		{
			var number = CommonUtil.GetRandomNumber(MinimumLimit, maximumLimit);
			return number;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameterScope"></param>
		/// <returns></returns>
		protected virtual int GetParameterWithScope(List<int> parameterScope)
		{
			Guard.ArgumentNotNull(parameterScope, "parameterScope");

			var number = CommonUtil.GetRandomNumber(parameterScope[0], parameterScope[1]);
			return number;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="leftParameter"></param>
		/// <returns></returns>
		protected virtual int GetRightParameter(int maximumLimit, int leftParameter = 0)
		{
			var number = CommonUtil.GetRandomNumber(MinimumLimit, maximumLimit - leftParameter);
			return number;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="leftParameter"></param>
		/// <param name="rightParameter"></param>
		/// <returns></returns>
		protected virtual int GetAnswer(int leftParameter, int rightParameter)
		{
			if (Formula == null)
			{
				throw new NullReferenceException(MessageUtil.GetException(() => MsgResources.E0023L));
			}

			switch (Formula.Sign)
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
					throw new ArgumentOutOfRangeException(MessageUtil.GetException(() => MsgResources.E0024L, Formula.Sign.ToString()));
			}
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public virtual Formula CreateFormula(CalculateParameter parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");

			Formula = new Formula();

			// 随机下限值
			MinimumLimit = parameter.MinimumLimit;
			// 默認填空項是答案項
			Formula.Gap = GapFilling.Answer;
			if (parameter.QuestionType == QuestionType.GapFilling)
			{
				// 對在等式中的三個數值隨機產生填空項（用於填空題型）
				Formula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Answer);
			}

			return Formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public virtual Formula CreateFormula(CalculateParameter parameter, Formula previousFormula)
		{
			Guard.ArgumentNotNull(parameter, "parameter");

			return CreateFormula(parameter);
		}

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public virtual Formula CreateFormulaWithAnswer(CalculateParameter parameter, int answer)
		{
			Guard.ArgumentNotNull(parameter, "parameter");

			return CreateFormula(parameter);
		}
	}
}