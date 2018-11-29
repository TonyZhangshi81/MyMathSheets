using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class TimeCalculationFormula
	{
		/// <summary>
		/// 計時制（AM/PM）
		/// </summary>
		public TimeSystem TimeType { get; set; }
		/// <summary>
		/// 時間段類型
		/// </summary>
		public TimeIntervalType TimeInterval { get; set; }
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
