using Common.Logging;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// LOG日誌功能實現類
	/// </summary>
	public class Log : ILog
	{
		/// <summary>
		/// 日誌實例作成
		/// </summary>
		static Log()
		{
			Instance = new Log();
		}

		/// <summary>
		/// 取得日誌實例
		/// </summary>
		public static ILog Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// 消息輸出
		/// </summary>
		/// <param name="message">消息</param>
		/// <param name="level">級別</param>
		public void Output(string message, MessageLevel level)
		{
			Output(message, level, null);
		}

		/// <summary>
		/// 消息輸出
		/// </summary>
		/// <param name="message">消息</param>
		/// <param name="level">級別</param>
		/// <param name="exception">異常對象</param>
		public void Output(string message, MessageLevel level, Exception exception)
		{
			Common.Logging.ILog logger = LogManager.GetLogger(GetType());
			switch (level)
			{
				case MessageLevel.Trace:
					if (logger.IsTraceEnabled)
					{
						logger.Trace(message, exception);
					}
					break;

				case MessageLevel.Debug:
					if (logger.IsDebugEnabled)
					{
						logger.Debug(message, exception);
					}
					break;

				case MessageLevel.Info:
					if (logger.IsInfoEnabled)
					{
						logger.Info(message, exception);
					}
					break;

				case MessageLevel.Warn:
					if (logger.IsWarnEnabled)
					{
						logger.Warn(message, exception);
					}
					break;

				case MessageLevel.Error:
					if (logger.IsErrorEnabled)
					{
						logger.Error(message, exception);
					}
					break;

				case MessageLevel.Fatal:
					if (logger.IsFatalEnabled)
					{
						logger.Fatal(message, exception);
					}
					break;
			}
		}

		/// <summary>
		/// 調試日誌輸出
		/// </summary>
		/// <param name="message">調試信息</param>
		public void Debug(string message)
		{
			LogUtil.LogDebug(message);
		}

		/// <summary>
		/// 調試日誌輸出
		/// </summary>
		/// <param name="message">調試信息</param>
		/// <param name="exception">異常對象</param>
		public void Debug(string message, Exception exception)
		{
			LogUtil.LogDebug(message, exception, null);
		}
	}
}