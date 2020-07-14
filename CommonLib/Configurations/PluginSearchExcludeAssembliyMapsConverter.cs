using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	///
	/// </summary>
	public class PluginSearchExcludeAssembliyMapsConverter : TypeConverter
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var data = value as string;
			if (data != null)
			{
				return this.GetPluginSearchExcludeAssembliyMaps(data);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="dataValue"></param>
		/// <returns></returns>
		private string[] GetPluginSearchExcludeAssembliyMaps(string dataValue)
		{
			var list = new string[] { };
			if (string.IsNullOrWhiteSpace(dataValue))
			{
				return list;
			}

			try
			{
				list = JsonConvert.DeserializeObject<string[]>(dataValue);
			}
			catch (Exception e)
			{
				throw new FormatException(MessageUtil.GetMessage(() => MsgResources.E0044L, dataValue), e);
			}

			return list;
		}
	}
}