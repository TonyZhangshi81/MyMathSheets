using MyMathSheets.CommonLib.Main.Item;
using System;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 貨幣類型轉換類
	/// </summary>
	public static class CurrencyUtil
	{
		/// <summary>
		/// 指定數值轉換為貨幣類型
		/// </summary>
		/// <param name="value">數值</param>
		/// <returns>貨幣類型</returns>
		public static CurrencyOperationUnitType IntToCurrencyUnitType(this int value)
		{
			string str = value.ToString();
			if (str.Length == 1)
			{
				return CurrencyOperationUnitType.Fen;
			}
			else if (str.Length == 2 && "0".Equals(str.Substring(1)))
			{
				return CurrencyOperationUnitType.Jiao;
			}
			else if (str.Length == 2 && !"0".Equals(str.Substring(1)))
			{
				return CurrencyOperationUnitType.JF;
			}
			else if (str.Length == 3 && "00".Equals(str.Substring(1)))
			{
				return CurrencyOperationUnitType.Yuan;
			}
			else if (str.Length == 3 && "0".Equals(str.Substring(1, 1)))
			{
				return CurrencyOperationUnitType.YF;
			}
			else if (str.Length == 3 && "0".Equals(str.Substring(2, 1)))
			{
				return CurrencyOperationUnitType.YJ;
			}
			else
			{
				return CurrencyOperationUnitType.YJF;
			}
		}

		/// <summary>
		/// 指定數值轉換為貨幣類型
		/// </summary>
		/// <param name="value">數值</param>
		/// <returns>貨幣類型</returns>
		public static Currency IntToCurrency(this int value)
		{
			Currency currency = new Currency
			{
				// 分
				Fen = value % 10,
				// 角
				Jiao = value % 100 / 10,
				// 元
				Yuan = value / 100
			};

			return currency;
		}
	}
}
