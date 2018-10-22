using log4net;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// LOG日誌功能實現類
	/// </summary>
	public class Log : ILogging
	{
		/// <summary>
		/// 
		/// </summary>
		static Log()
		{
		}

		/// <summary>
		/// ログ出力用のインスタンスを公開します。
		/// </summary>
		public static ILog Instance
		{
			get;
			private set;
		}

		public static Log LogReady(Type type)
		{
			Instance = LogManager.GetLogger(type);
			return new Log();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Debug(string message)
		{
			this.Output(message, MessageLevel.Debug, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public void Debug(string message, Exception exception)
		{
			this.Output(message, MessageLevel.Error, exception);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Output(string message)
		{
			this.Output(message, MessageLevel.Info, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public void Output(string message, Exception exception)
		{
			this.Output(message, MessageLevel.Error, exception);
		}

		/// <summary>
		/// メッセージを、その出力レベルに合わせてログに出力します。
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="level">出力レベル</param>
		/// <param name="exception">例外情報</param>
		internal void Output(object message, MessageLevel level, Exception exception)
		{
			var logger = Instance;
			switch (level)
			{
				case MessageLevel.Debug:
					if (logger.IsDebugEnabled)
					{
						logger.Debug(message, exception);
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
			}
		}
	}
}
