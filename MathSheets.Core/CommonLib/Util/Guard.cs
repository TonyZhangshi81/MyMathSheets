using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 參數判定
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// 指定參數類型對象是否為NULL
		/// </summary>
		/// <typeparam name="T">類型</typeparam>
		/// <param name="argument">對象</param>
		/// <param name="parameterName">參數名</param>
		public static void ArgumentNotNull<T>(T argument, string parameterName) where T : class
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(MessageUtil.GetMessage(() => MsgResources.E0037L));
			}
			if (argument == null)
			{
				throw new ArgumentNullException(parameterName, MessageUtil.GetMessage(() => MsgResources.E0036L, parameterName));
			}
		}

		/// <summary>
		/// 指定參數類型對象是否為空
		/// </summary>
		/// <param name="argument">對象</param>
		/// <param name="parameterName">參數名</param>
		public static void ArgumentNotEmpty(string argument, string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(MessageUtil.GetMessage(() => MsgResources.E0037L));
			}
			if (string.IsNullOrEmpty(argument))
			{
				throw new ArgumentNullException(parameterName, MessageUtil.GetMessage(() => MsgResources.E0036L, parameterName));
			}
		}
	}
}