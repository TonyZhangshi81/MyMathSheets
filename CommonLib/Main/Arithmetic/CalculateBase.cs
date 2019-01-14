using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;

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
		protected int _minimumLimit { get; set; }
		/// <summary>
		/// 
		/// </summary>
		protected Formula _formula { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected virtual int GetLeftParameter(int maximumLimit)
		{
			var number = CommonUtil.GetRandomNumber(_minimumLimit, maximumLimit);
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
			var number = CommonUtil.GetRandomNumber(_minimumLimit, maximumLimit - leftParameter);
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
			if (_formula == null)
			{
				throw new NullReferenceException(MessageUtil.GetException(() => MsgResources.E0023L));
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
					throw new ArgumentOutOfRangeException(MessageUtil.GetException(() => MsgResources.E0024L, _formula.Sign.ToString()));
			}
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public virtual Formula CreateFormula(CalculateParameter parameter)
		{
			_formula = new Formula();

			// 随机下限值
			_minimumLimit = parameter.MinimumLimit;
			// 默認填空項是答案項
			_formula.Gap = GapFilling.Answer;
			// 默認計算式不帶小括號
			_formula.IsNeedBracket = false;
			if (parameter.QuestionType == QuestionType.GapFilling)
			{
				// 對在等式中的三個數值隨機產生填空項（用於填空題型）
				_formula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Answer);
			}

			return _formula;
		}


		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public virtual Formula CreateFormula(CalculateParameter parameter, Formula previousFormula)
		{
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
			return CreateFormula(parameter);
		}
	}
}
