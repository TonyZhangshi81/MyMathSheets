using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// 自定義類型轉換器（特定的字符串配置格式）以獲得對參數的補充
	/// </summary>
	/// <remarks>
	/// 字符串的特定組織形式
	/// AssemblyName01#Param01:Value01,Param02:Value02,...;AssemblyName02#Param01:Value01,Param02:Value02....
	/// <seealso cref="string">
	/// MathWordProblems#ProblemsJsonFilePath:~\Config\MathWordProblemsLibrary.json;
	/// </seealso>
	/// </remarks>
	public sealed class ReplenishArgumentsMapsConverter : TypeConverter
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// 以特定的字符串配置格式進行類型<see cref="Dictionary{TKey, TValue}"/>轉換
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
				return GetReplenishArgumentsMaps(data);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// 特定的字符串配置格式
		/// </summary>
		/// <param name="dataValue">轉換內容</param>
		/// <returns>轉換後結果</returns>
		/// <remarks>
		/// 字符串的特定組織形式
		/// AssemblyName01#Param01:Value01,Param02:Value02,...;AssemblyName02#Param01:Value01,Param02:Value02....
		/// <seealso cref="string">
		/// MathWordProblems#ProblemsJsonFilePath:~\Config\MathWordProblemsLibrary.json;
		/// </seealso>
		/// </remarks>
		private static Dictionary<string, Dictionary<string, string>> GetReplenishArgumentsMaps(string dataValue)
		{
			if (string.IsNullOrWhiteSpace(dataValue))
			{
				return null;
			}

			var topic = dataValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			if (topic.Length == 0)
			{
				return null;
			}

			var replenishArgumentsMap = new Dictionary<string, Dictionary<string, string>>();
			topic.ToList().ForEach(t =>
			{
				var replenish = t.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
				if (replenish.Length != 0)
				{
					var paramDict = new Dictionary<string, string>();

					var parameters = replenish[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
					if (parameters.Length != 0)
					{
						parameters.ToList().ForEach(p =>
						{
							var parameter = p.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
							paramDict.Add(parameter[0], parameter[1]);
						});
					}

					replenishArgumentsMap.Add(replenish[0], paramDict);
				}
			});
			return replenishArgumentsMap;
		}
	}
}