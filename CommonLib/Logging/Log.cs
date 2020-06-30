using Common.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
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
		///
		/// </summary>
		/// <param name="message"></param>
		public void Debug(string message)
		{
			Output(message, MessageLevel.Debug, null);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public void Debug(string message, Exception exception)
		{
			Output(message, MessageLevel.Error, exception);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		/// <param name="bcp"></param>
		public void Debug(string message, Exception exception, CalculateParameter bcp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		/// <param name="bcp"></param>
		public void Debug(string message, Exception exception, ParameterBase bcp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="exception"></param>
		internal void Output(object message, MessageLevel level, Exception exception)
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
	}
}