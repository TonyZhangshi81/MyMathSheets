using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// 自定義類型轉換器（JSON轉字符串集合）以獲取需要忽略程序集集合
	/// </summary>
	public sealed class PluginSearchExcludeAssembliyMapsConverter : TypeConverter
	{
		/// <summary>
		/// JSON轉字符串集合
		/// </summary>
		/// <param name="context">提供有關組件的上下文信息</param>
		/// <param name="culture">區域信息</param>
		/// <param name="value">轉換內容</param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				var data = value as string;
				return GetPluginSearchExcludeAssembliyMaps(data);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// JSON轉字符串集合的方法實現
		/// </summary>
		/// <param name="dataValue">轉換內容</param>
		/// <returns>轉換後結果</returns>
		private static List<string> GetPluginSearchExcludeAssembliyMaps(string dataValue)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrWhiteSpace(dataValue))
			{
				return list;
			}

			try
			{
				list = JsonConvert.DeserializeObject<List<string>>(dataValue);
			}
			catch (Exception e)
			{
				throw new FormatException(MessageUtil.GetMessage(() => MsgResources.E0044L, dataValue), e);
			}

			return list;
		}
	}
}