using System;

namespace MyMathSheets.CommonLib.Main.Item
{
	/// <summary>
	/// 時間對象(解決兩個時間之間的差值)
	/// </summary>
	public class Time
	{
		/// <summary>
		/// 開始時間
		/// </summary>
		public DateTime StartTime { get; set; }
		/// <summary>
		/// 結束時間
		/// </summary>
		public DateTime EndTime { get; set; }
		/// <summary>
		/// 小時數
		/// </summary>
		public int? Hours { get; set; }
		/// <summary>
		/// 分鐘數
		/// </summary>
		public int? Minutes { get; set; }
		/// <summary>
		/// 秒數
		/// </summary>
		public int? Seconds { get; set; }
	}
}
