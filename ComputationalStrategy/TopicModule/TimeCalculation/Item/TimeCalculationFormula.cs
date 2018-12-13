﻿using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.TimeCalculation.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class TimeCalculationFormula
	{
		/// <summary>
		/// 計算開始時間
		/// </summary>
		public Time StartTime { get; set; }
		/// <summary>
		/// 計算結束時間
		/// </summary>
		public Time EndTime { get; set; }
		/// <summary>
		/// 經過的時間
		/// </summary>
		public Time ElapsedTime { get; set; }
	}
}