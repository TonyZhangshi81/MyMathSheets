using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// 
	/// </summary>
	public class NLogger : Common.Logging.Factory.AbstractLogger
	{
		/// <summary>
		/// NLog对象
		/// </summary>
		private readonly NLog.Logger _logger;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public NLogger(string name)
		{
			_logger = NLog.LogManager.GetLogger(name);
		}

		/// <summary>
		/// 对调试信息的日志处理是否有效
		/// </summary>
		public override bool IsDebugEnabled
		{
			get { return _logger.IsDebugEnabled; }
		}

		/// <summary>
		/// 对错误信息的日志处理是否有效
		/// </summary>
		public override bool IsErrorEnabled
		{
			get { return _logger.IsErrorEnabled; }
		}

		/// <summary>
		/// 对致命错误信息的日志处理是否有效
		/// </summary>
		public override bool IsFatalEnabled
		{
			get { return _logger.IsFatalEnabled; }
		}

		/// <summary>
		/// 对信息类型消息的日志处理是否有效
		/// </summary>
		public override bool IsInfoEnabled
		{
			get { return _logger.IsInfoEnabled; }
		}

		/// <summary>
		/// 对日常消息的日志处理是否有效
		/// </summary>
		public override bool IsTraceEnabled
		{
			get { return _logger.IsTraceEnabled; }
		}

		/// <summary>
		/// 对警告消息的日志处理是否有效
		/// </summary>
		public override bool IsWarnEnabled
		{
			get { return _logger.IsWarnEnabled; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="level"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		protected override void WriteInternal(Common.Logging.LogLevel level, object message, Exception exception)
		{
			NLog.LogLevel nlogLevel = this.GetLevel(level);
			NLog.LogEventInfo logEvent = new NLog.LogEventInfo(nlogLevel, _logger.Name, null, "{0}", new object[] { message }, exception);
			_logger.Log(logEvent);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		private NLog.LogLevel GetLevel(Common.Logging.LogLevel level)
		{
			switch (level)
			{
				case Common.Logging.LogLevel.All:
					return NLog.LogLevel.Trace;
				case Common.Logging.LogLevel.Trace:
					return NLog.LogLevel.Trace;
				case Common.Logging.LogLevel.Debug:
					return NLog.LogLevel.Debug;
				case Common.Logging.LogLevel.Info:
					return NLog.LogLevel.Info;
				case Common.Logging.LogLevel.Warn:
					return NLog.LogLevel.Warn;
				case Common.Logging.LogLevel.Error:
					return NLog.LogLevel.Error;
				case Common.Logging.LogLevel.Fatal:
					return NLog.LogLevel.Fatal;
				case Common.Logging.LogLevel.Off:
					return NLog.LogLevel.Off;
				default:
					return NLog.LogLevel.Trace;
			}
		}
	}
}
