namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string OperationToID(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "P";
					break;
				case SignOfOperation.Subtraction:
					flag = "S";
					break;
				case SignOfOperation.Division:
					flag = "D";
					break;
				case SignOfOperation.Multiple:
					flag = "M";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToOperationString(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "+";
					break;
				case SignOfOperation.Subtraction:
					flag = "-";
					break;
				case SignOfOperation.Division:
					flag = "÷";
					break;
				case SignOfOperation.Multiple:
					flag = "×";
					break;
				default:
					break;
			}
			return flag;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToSignOfCompareString(this SignOfCompare operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfCompare.Equal:
					flag = "=";
					break;
				case SignOfCompare.Greater:
					flag = ">";
					break;
				case SignOfCompare.Less:
					flag = "<";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 關係運算符enum轉圖片名稱（HTML圖片顯示使用）
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToSignOfCompareEnString(this SignOfCompare operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfCompare.Equal:
					flag = "calculator";
					break;
				case SignOfCompare.Greater:
					flag = "char-more";
					break;
				case SignOfCompare.Less:
					flag = "char-less";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 計時制（AM/PM）enum轉圖片名稱（HTML圖片顯示使用）
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ToTimeSystemString(this TimeSystem type)
		{
			var flag = string.Empty;
			switch (type)
			{
				case TimeSystem.AM:
					flag = "Sun";
					break;
				case TimeSystem.PM:
					flag = "Moon";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 時間段類型（24小時制）enum轉tooltip（HTML圖片顯示使用）
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string ToTimeIntervalTypeString(this TimeIntervalType type)
		{
			var flag = string.Empty;
			switch (type)
			{
				case TimeIntervalType.Midnight:
					flag = "午夜";
					break;
				case TimeIntervalType.WeeHours:
					flag = "凌晨";
					break;
				case TimeIntervalType.Forenoon:
					flag = "上午";
					break;
				case TimeIntervalType.Nooning:
					flag = "中午";
					break;
				case TimeIntervalType.Afternoon:
					flag = "下午";
					break;
				case TimeIntervalType.Night:
					flag = "晚上";
					break;
				case TimeIntervalType.LateNight:
					flag = "深夜";
					break;
				default:
					break;
			}
			return flag;
		}
	}
}
