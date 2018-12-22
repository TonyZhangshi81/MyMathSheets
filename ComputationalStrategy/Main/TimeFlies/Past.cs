using MyMathSheets.BasicOperationsLib.Properties;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.BasicOperationsLib.Main.TimeFlies
{
	/// <summary>
	/// 過去的時間
	/// </summary>
	[Calculate(SignOfOperation.Before)]
	public class Past : CalculateBase
	{
		/// <summary>
		/// 求時間的計算結果
		/// </summary>
		/// <param name="leftParameter">開始時間(秒數)</param>
		/// <param name="rightParameter">進過時間(秒數)</param>
		/// <returns>秒數</returns>
		protected override int GetAnswer(int leftParameter, int rightParameter)
		{
			if (_formula == null)
			{
				throw new NullReferenceException(MessageUtil.GetException(() => MsgResources.E0001L));
			}

			// 開始時間
			DateTime startTime = leftParameter.ToDateTime();
			DateTime endTime = startTime.AddSeconds(rightParameter * -1);
			return endTime.ToSeconds();
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			_formula = base.CreateFormula(parameter);

			_formula.LeftParameter = parameter.MaximumLimit;
			_formula.Sign = SignOfOperation.Subtraction;
			_formula.RightParameter = parameter.MinimumLimit;
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter, Formula previousFormula)
		{
			return CreateFormula(parameter);
		}

		/// <summary>
		/// 由計算結果推算出計算式
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(CalculateParameter parameter, int answer)
		{
			return CreateFormula(parameter);
		}
	}
}
