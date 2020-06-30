using MyMathSheets.CommonLib.Main.Item;
using System;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 時間類型轉換類
	/// </summary>
	public static class TimeUtil
	{
		/// <summary>
		/// 時間日期類型取時間
		/// </summary>
		/// <param name="dt">時間日期</param>
		/// <returns>時間</returns>
		public static Time ToTime(this DateTime dt)
		{
			return new Time() { Hours = dt.Hour, Minutes = dt.Minute, Seconds = dt.Second };
		}

		/// <summary>
		/// 時間日期類型轉秒數
		/// </summary>
		/// <param name="dt">時間日期</param>
		/// <returns>秒數</returns>
		public static int ToSeconds(this DateTime dt)
		{
			return dt.ToTime().ToSeconds();
		}

		/// <summary>
		/// 秒數轉換為時間
		/// </summary>
		/// <param name="seconds">秒數</param>
		/// <returns>轉換后的時間</returns>
		public static DateTime ToDateTime(this int seconds)
		{
			DateTime dt = new DateTime(1970, 1, 1);
			dt = dt.AddSeconds(seconds);
			return dt;
		}

		/// <summary>
		/// 秒數轉換為時間
		/// </summary>
		/// <param name="seconds">秒數</param>
		/// <returns>轉換后的時間</returns>
		public static Time ToTime(this int seconds)
		{
			DateTime dt = seconds.ToDateTime();
			return dt.ToTime();
		}

		/// <summary>
		/// 時間轉換為秒數
		/// </summary>
		/// <param name="dt">時間</param>
		/// <returns>轉換后的秒數</returns>
		public static int ToSeconds(this Time dt)
		{
			return CommonUtil.Time2Second(dt.Hours ?? 0, dt.Minutes ?? 0, dt.Seconds ?? 0);
		}
	}
}