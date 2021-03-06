﻿using System;
using System.Threading;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	///
	/// </summary>
	public static class ConvertHelper
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="conversionType"></param>
		/// <returns></returns>
		public static object ChangeType(object obj, Type conversionType)
		{
			return ChangeType(obj, conversionType, Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="conversionType"></param>
		/// <param name="provider"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="obj"/>為NULL的情況</exception>
		public static object ChangeType(object obj, Type conversionType, IFormatProvider provider)
		{
			Type nullableType = Nullable.GetUnderlyingType(conversionType);
			if (nullableType != null)
			{
				return obj == null ? null : Convert.ChangeType(obj, nullableType, provider);
			}

			Guard.ArgumentNotNull(obj, "obj");

			return typeof(Enum).IsAssignableFrom(conversionType)
				? Enum.Parse(conversionType, obj.ToString())
				: Convert.ChangeType(obj, conversionType, provider);
		}
	}
}