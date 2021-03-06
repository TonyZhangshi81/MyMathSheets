﻿using MyMathSheets.BasicOperationsLib.Properties;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Calculate.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.BasicOperationsLib.Main.TimeFlies
{
	/// <summary>
	/// 將來的時間
	/// </summary>
	[Arithmetic(SignOfOperation.Later)]
	public class Future : ArithmeticBase
	{
		/// <summary>
		/// 求時間的計算結果
		/// </summary>
		/// <param name="leftParameter">開始時間(秒數)</param>
		/// <param name="rightParameter">進過時間(秒數)</param>
		/// <returns>秒數</returns>
		protected override int GetAnswer(int leftParameter, int rightParameter)
		{
			if (Formula == null)
			{
				throw new NullReferenceException(MessageUtil.GetMessage(() => MsgResources.E0001L));
			}

			// 開始時間
			DateTime startTime = leftParameter.ToDateTime();
			DateTime endTime = startTime.AddSeconds(rightParameter);
			return endTime.ToSeconds();
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(ArithmeticParameter parameter)
		{
			Formula = base.CreateFormula(parameter);

#pragma warning disable CA1062 // base.CreateFormula已對parameter進行NULL判定處理
			Formula.LeftParameter = parameter.MaximumLimit;
#pragma warning restore CA1062
			Formula.Sign = SignOfOperation.Plus;
			Formula.RightParameter = parameter.MinimumLimit;
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

			return Formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(ArithmeticParameter parameter, Formula previousFormula)
		{
			return CreateFormula(parameter);
		}

		/// <summary>
		/// 由計算結果推算出計算式
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(ArithmeticParameter parameter, int answer)
		{
			return CreateFormula(parameter);
		}
	}
}