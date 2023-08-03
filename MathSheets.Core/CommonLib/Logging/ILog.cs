using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// LOG日誌接口類
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// 消息輸出
		/// </summary>
		/// <param name="message">消息</param>
		/// <param name="level">級別</param>
		void Output(string message, MessageLevel level);

		/// <summary>
		/// 消息輸出
		/// </summary>
		/// <param name="message">消息</param>
		/// <param name="level">級別</param>
		/// <param name="exception">異常對象</param>
		void Output(string message, MessageLevel level, Exception exception);

		/// <summary>
		/// 調試日誌輸出
		/// </summary>
		/// <param name="message">調試信息</param>
		void Debug(string message);

		/// <summary>
		/// 調試日誌輸出
		/// </summary>
		/// <param name="message">調試信息</param>
		/// <param name="exception">異常對象</param>
		void Debug(string message, Exception exception);
	}
}