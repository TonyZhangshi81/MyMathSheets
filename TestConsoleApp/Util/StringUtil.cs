using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TestConsoleApp.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string TimeIntervalTypeToString(this TimeIntervalType type)
		{
			var flag = string.Empty;
			switch (type)
			{
				case TimeIntervalType.Midnight:
					flag = "午夜[0:XX]";
					break;
				case TimeIntervalType.WeeHours:
					flag = "凌晨[1:XX~5:XX]";
					break;
				case TimeIntervalType.Forenoon:
					flag = "上午[6:XX~11:XX]";
					break;
				case TimeIntervalType.Nooning:
					flag = "中午[12:XX]";
					break;
				case TimeIntervalType.Afternoon:
					flag = "下午[13:XX~18:XX]";
					break;
				case TimeIntervalType.Night:
					flag = "晚上[19:XX~21:XX]";
					break;
				case TimeIntervalType.LateNight:
					flag = "深夜[22:XX~23:XX]";
					break;
				default:
					break;
			}
			return flag;
		}
	}
}
