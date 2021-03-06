﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// JSON公共處理類(Newtonsoft 類庫)
	/// </summary>
	public static class JsonExtension
	{
		/// <summary>
		/// 將指定對象轉換為JSON字符串
		/// </summary>
		/// <typeparam name="T">指定參數對象類型</typeparam>
		/// <param name="transObject">指定參數對象</param>
		/// <returns>JSON字符串</returns>
		public static string GetJsonByObject<T>(this T transObject)
		{
			if (null == transObject)
			{
				return string.Empty;
			}

			return JsonConvert.SerializeObject(transObject, typeof(T), null);
		}

		/// <summary>
		/// 取得JSON字符串中指定的屬性值
		/// </summary>
		/// <param name="jsonString">JSON字符串</param>
		/// <param name="propertyName">屬性名稱</param>
		/// <returns>屬性值</returns>
		public static object GetPropertyByJson(this string jsonString, string propertyName)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return null;
			}

			JObject jObj = JObject.Parse(jsonString);

			return jObj[propertyName];
		}

		/// <summary>
		/// 指定屬性是否存在
		/// </summary>
		/// <param name="jsonString">JSON字符串</param>
		/// <param name="propertyName">屬性名稱</param>
		/// <returns>存在:TRUE</returns>
		public static bool ContainsKey(this string jsonString, string propertyName)
		{
			JObject jObj = JObject.Parse(jsonString);
			return jObj.ContainsKey(propertyName);
		}

		/// <summary>
		/// 將JSON字符串轉換為對象
		/// </summary>
		/// <typeparam name="T">轉換後的對象類型</typeparam>
		/// <param name="jsonString">JSON字符串</param>
		/// <returns>轉換後的對象</returns>
		public static T GetObjectByJson<T>(this string jsonString)
		{
			return JsonConvert.DeserializeObject<T>(jsonString);
		}
	}
}