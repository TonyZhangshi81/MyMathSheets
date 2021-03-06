﻿using MyMathSheets.CommonLib.Main.Item;
using System;
using System.Globalization;
using System.Text;

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
			string str = value.ToString(CultureInfo.CurrentCulture);
			if (str.Length == 1)
			{
				return CurrencyOperationUnitType.Fen;
			}
			else if (str.Length == 2 && "0".Equals(str.Substring(1), StringComparison.CurrentCultureIgnoreCase))
			{
				return CurrencyOperationUnitType.Jiao;
			}
			else if (str.Length == 2 && !"0".Equals(str.Substring(1), StringComparison.CurrentCultureIgnoreCase))
			{
				return CurrencyOperationUnitType.JF;
			}
			else if (str.Length == 3 && "00".Equals(str.Substring(1), StringComparison.CurrentCultureIgnoreCase))
			{
				return CurrencyOperationUnitType.Yuan;
			}
			else if (str.Length == 3 && "0".Equals(str.Substring(1, 1), StringComparison.CurrentCultureIgnoreCase))
			{
				return CurrencyOperationUnitType.YF;
			}
			else if (str.Length == 3 && "0".Equals(str.Substring(2, 1), StringComparison.CurrentCultureIgnoreCase))
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
				Fen = (value % 10 == 0) ? (int?)null : (value % 10),
				// 角
				Jiao = (value % 100 / 10 == 0) ? (int?)null : (value % 100 / 10),
				// 元
				Yuan = (value / 100 == 0) ? (int?)null : (value / 100)
			};

			return currency;
		}

		/// <summary>
		/// 貨幣元角分已字符串形式輸出
		/// </summary>
		/// <param name="currency">貨幣對象</param>
		/// <returns>字符串</returns>
		/// <exception cref="ArgumentNullException"><paramref name="currency"/>為NULL的情況</exception>
		public static string CurrencyToString(this Currency currency)
		{
			Guard.ArgumentNotNull(currency, "currency");

			StringBuilder builder = new StringBuilder();

			// 元單位信息打印
			if (currency.Yuan.HasValue)
			{
				builder.AppendFormat("{0}{1}", currency.Yuan, Consts.YUAN_UNIT);
			}
			// 角單位信息打印
			if (currency.Jiao.HasValue)
			{
				builder.AppendFormat("{0}{1}", currency.Jiao, Consts.JIAO_UNIT);
			}
			// 分單位信息打印
			if (currency.Fen.HasValue)
			{
				builder.AppendFormat("{0}{1}", currency.Fen, Consts.FEN_UNIT);
			}
			return builder.ToString();
		}
	}
}