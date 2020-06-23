using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TestConsoleApp.Util
{
	/// <summary>
	/// 特定類型轉換為字符串
	/// </summary>
	public static class StringUtil
	{
		/// <summary>
		/// 獲取運算符對應的簡寫ID
		/// </summary>
		/// <param name="operation">運算符</param>
		/// <returns>簡寫ID</returns>
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
				case SignOfOperation.Before:
					flag = "之前";
					break;
				case SignOfOperation.Later:
					flag = "之後";
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
		/// 24小時制轉化為特定格式字符串
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

		/// <summary>
		/// 貨幣值轉換為貨幣類型字符串
		/// </summary>
		/// <param name="type">貨幣值</param>
		/// <returns>貨幣類型字符串</returns>
		public static string CurrencyOperationUnitTypeToString(this int value)
		{
			CurrencyOperationUnitType type = value.IntToCurrencyUnitType();
			Currency currency = value.IntToCurrency();

			switch(type)
			{
				case CurrencyOperationUnitType.Fen:
					return string.Format("{0}分", currency.Fen);
				case CurrencyOperationUnitType.JF:
					return string.Format("{0}角{1}分", currency.Jiao, currency.Fen);
				case CurrencyOperationUnitType.Jiao:
					return string.Format("{0}角", currency.Jiao);
				case CurrencyOperationUnitType.YF:
					return string.Format("{0}元{1}分", currency.Yuan, currency.Fen);
				case CurrencyOperationUnitType.YJ:
					return string.Format("{0}元{1}角", currency.Yuan, currency.Jiao);
				case CurrencyOperationUnitType.YJF:
					return string.Format("{0}元{1}角{2}分", currency.Yuan, currency.Jiao, currency.Fen);
				case CurrencyOperationUnitType.Yuan:
					return string.Format("{0}元", currency.Yuan);
				default:
					return string.Empty;
			}
		}
	}
}
