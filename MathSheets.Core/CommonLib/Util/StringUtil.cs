namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 字符串轉換類
	/// </summary>
	public static class StringUtil
	{
		/// <summary>
		/// 運算符轉換為對應的字符編碼
		/// </summary>
		/// <param name="operation">運算符(加減乘除)</param>
		/// <returns>字符編碼</returns>
		public static string ToOperationUnicode(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				// +
				case SignOfOperation.Plus:
					flag = "&#43;";
					break;
				// −
				case SignOfOperation.Subtraction:
					flag = "&#8722;";
					break;
				// ÷
				case SignOfOperation.Division:
					flag = "&#247;";
					break;
				// ×
				case SignOfOperation.Multiple:
					flag = "&#215;";
					break;
				// 之前
				case SignOfOperation.Before:
					flag = "&#20043;&#21069;";
					break;
				// 之后
				case SignOfOperation.Later:
					flag = "&#20043;&#21518;";
					break;

				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 運算符轉換為對應的字符編碼
		/// </summary>
		/// <param name="operation">運算符(加減乘除)</param>
		/// <returns>字符編碼</returns>
		public static string ToOperationString(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				// +
				case SignOfOperation.Plus:
					flag = "+";
					break;
				// −
				case SignOfOperation.Subtraction:
					flag = "-";
					break;
				// ÷
				case SignOfOperation.Division:
					flag = "/";
					break;
				// ×
				case SignOfOperation.Multiple:
					flag = "*";
					break;
				// 之前
				case SignOfOperation.Before:
					flag = "之前";
					break;
				// 之后
				case SignOfOperation.Later:
					flag = "之后";
					break;

				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 關係運算符轉換為對應的字符編碼
		/// </summary>
		/// <param name="operation">運算符(大於小於等於)</param>
		/// <returns>字符編碼</returns>
		public static string ToSignOfCompareUnicode(this SignOfCompare operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				// =
				case SignOfCompare.Equal:
					flag = "&#61;";
					break;
				// >
				case SignOfCompare.Greater:
					flag = "&#62;";
					break;
				// <
				case SignOfCompare.Less:
					flag = "&#60;";
					break;

				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 關係運算符轉換為對應的字符編碼
		/// </summary>
		/// <param name="operation">運算符(大於小於等於)</param>
		/// <returns>字符編碼</returns>
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
		/// <param name="operation">運算符(大於小於等於)</param>
		/// <returns>圖片名稱</returns>
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
		public static string ToTimeSystemString(this TimeSystemType type)
		{
			var flag = string.Empty;
			switch (type)
			{
				case TimeSystemType.AM:
					flag = "Sun";
					break;

				case TimeSystemType.PM:
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

		/// <summary>
		/// 題型分類的名稱
		/// </summary>
		/// <param name="classify">題型分類</param>
		/// <returns>題型分類的名稱</returns>
		public static string ToClassifyName(this LayoutSetting.Classify classify)
		{
			string name = string.Empty;
			switch (classify)
			{
				case LayoutSetting.Classify.Generally:
					name = "一般计算";
					break;

				case LayoutSetting.Classify.Time:
					name = "时间单位";
					break;

				case LayoutSetting.Classify.Extent:
					name = "长度单位";
					break;

				case LayoutSetting.Classify.Currency:
					name = "货币单位";
					break;

				default:
					break;
			}

			return name;
		}
	}
}