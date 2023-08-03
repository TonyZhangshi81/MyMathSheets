using MyMathSheets.CommonLib.Util;
using System.Globalization;

namespace MyMathSheets.CommonLib.Main.Item
{
	/// <summary>
	/// 時間對象(解決兩個時間之間的差值)
	/// </summary>
	public class TimeType
	{
		/// <summary>
		/// 時間段類型
		/// </summary>
		public TimeIntervalType TimeInterval
		{
			get
			{
				if (!Hours.HasValue)
				{
					return TimeIntervalType.Overflow;
				}

				switch (Hours.Value)
				{
					case 0:
						return TimeIntervalType.Midnight;

					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						return TimeIntervalType.WeeHours;

					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
						return TimeIntervalType.Forenoon;

					case 12:
						return TimeIntervalType.Nooning;

					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
						return TimeIntervalType.Afternoon;

					case 19:
					case 20:
					case 21:
						return TimeIntervalType.Night;

					case 22:
					case 23:
						return TimeIntervalType.LateNight;

					default:
						return TimeIntervalType.Overflow;
				}
			}
		}

		/// <summary>
		/// 計時制（AM/PM）
		/// </summary>
		public TimeSystemType GetTimeType
		{
			get
			{
				if (!Hours.HasValue || !Minutes.HasValue)
				{
					return TimeSystemType.Overflow;
				}

				// 上午[00:01~12:00]
				if ((0 < Hours && Hours < 12)
					|| (Hours == 12 && Minutes == 0)
					|| (Hours == 0 && Minutes >= 1))
				{
					return TimeSystemType.AM;
				}
				else
				{
					return TimeSystemType.PM;
				}
			}
		}

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

		/// <summary>
		/// 返回時間
		/// </summary>
		public string HMSValue
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "{0}:{1}:{2}"
									, Hours.Value.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0')
									, Minutes.Value.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0')
									, Seconds.Value.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0'));
			}
		}

		/// <summary>
		/// 返回時間
		/// </summary>
		public string CNValue
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "{0}小時{1}分鐘{2}秒", Hours.Value, Minutes.Value, Seconds.Value);
			}
		}
	}
}