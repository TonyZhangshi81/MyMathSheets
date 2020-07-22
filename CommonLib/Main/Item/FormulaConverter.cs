using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// 自定義類型轉換器（<see cref="Formula"/>對象字符串化）
	/// </summary>
	public sealed class FormulaConverter : TypeConverter
	{
		/// <summary>
		/// 運算符類型對應關係表
		/// </summary>
		private readonly Dictionary<string, SignOfOperation> signMap = new Dictionary<string, SignOfOperation>() {
																						{ "+", SignOfOperation.Plus},
																						{ "-", SignOfOperation.Subtraction},
																						{ "*", SignOfOperation.Multiple},
																						{ "/", SignOfOperation.Division}
																					};

		/// <summary>
		/// 指定參數是否支持轉換
		/// </summary>
		/// <param name="context">提供有關組件的上下文信息</param>
		/// <param name="sourceType">裝還源</param>
		/// <returns>允許：TRUE</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(Formula) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// <see cref="Formula"/>對象轉字符串化
		/// </summary>
		/// <param name="context">提供有關組件的上下文信息</param>
		/// <param name="culture">區域信息</param>
		/// <param name="value">轉換內容</param>
		/// <param name="destinationType">目標類型</param>
		/// <returns>字符串化</returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (!(value is Formula formula) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}

			return string.Format(CultureInfo.CurrentCulture, "{0}{1}{2}={3}", formula.LeftParameter, formula.Sign.ToOperationString(), formula.RightParameter, formula.Answer);
		}

		/// <summary>
		/// 指定參數是否支持轉換
		/// </summary>
		/// <param name="context">提供有關組件的上下文信息</param>
		/// <param name="sourceType">裝還源</param>
		/// <returns>允許：TRUE</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// 字符串轉<see cref="Formula"/>對象
		/// </summary>
		/// <param name="context">提供有關組件的上下文信息</param>
		/// <param name="culture">區域信息</param>
		/// <param name="value">轉換內容</param>
		/// <returns><see cref="Formula"/>對象</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			string formula = value as string;
			if (string.IsNullOrEmpty(formula))
			{
				return base.ConvertFrom(context, culture, value);
			}

			var items = formula.Split(new char[] { '+', '-', '*', '/', '=' }, StringSplitOptions.RemoveEmptyEntries);
			var cursor = formula.IndexOfAny(new char[] { '+', '-', '*', '/' });
			var sign = formula.Substring(cursor, 1);

			return new Formula(Convert.ToInt32(items[0], CultureInfo.CurrentCulture)
								, Convert.ToInt32(items[1], CultureInfo.CurrentCulture)
								, Convert.ToInt32(items[2], CultureInfo.CurrentCulture)
								, signMap[sign]);
		}
	}
}