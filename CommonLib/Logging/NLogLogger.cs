using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using NLog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// NLOG日誌處理類
	/// </summary>
	public class NLogLogger : Common.Logging.NLog.NLogLogger
	{
		/// <summary>
		/// 日誌處理對象實例
		/// </summary>
		private readonly Logger _logger;

		/// <summary>
		/// 不參與日誌處理的模塊
		/// </summary>
		private static readonly Assembly[] ExcludeAssemblies =
			new[]
				{
					typeof(Common.Logging.ILog).Assembly,
					typeof(Common.Logging.NLog.NLogLogger).Assembly
				};

		/// <summary>
		/// 不參與日誌處理的類
		/// </summary>
		private static readonly Type[] ExcludeTypes =
			new[]
				{
					typeof(NLogLogger),
					typeof(NLogLoggerFactoryAdapter),
					typeof(LogUtil)
				};

		/// <summary>
		/// 不參與日誌處理的命名空間
		/// </summary>
		private static readonly string[] ExcludeNameSpaces =
			new[]
				{
					"MyMathSheets.CommonLib.Logging"
				};

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
		/// 構造函數
		/// </summary>
		/// <param name="logger">日誌處理名稱</param>
		public NLogLogger(Logger logger)
			: base(logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// 消息寫入日誌的詳細處理
		/// </summary>
		/// <param name="logLevel">日誌處理級別</param>
		/// <param name="message">日誌信息<see cref="ILogContext"/>自定義的屬性消息，寫入到<see cref="LogEventInfo.Properties"/></param>
		/// <param name="exception">異常對象</param>
		protected override void WriteInternal(Common.Logging.LogLevel logLevel, object message, Exception exception)
		{
			LogLevel level = GetLevel(logLevel);
			ILogContext context = message as ILogContext;
			object messageText = context == null ? message : context.Message;
			LogEventInfo logEvent = new LogEventInfo(level, _logger.Name, null, "{0}", new object[1] { messageText }, exception);
			context?.Properties.ToList().ForEach(logEvent.Properties.Add);
			StackTrace st = new StackTrace(0);
			int index = 0;
			StackFrame[] frames = st.GetFrames();
			foreach (StackFrame sf in frames)
			{
				MethodBase mb = sf.GetMethod();
				Type dt = mb.DeclaringType;
				if (dt == null || ExcludeTypes.Contains(dt) || ExcludeAssemblies.Contains(dt.Assembly) || ExcludeNameSpaces.Contains(dt.Namespace))
				{
					index++;
					continue;
				}
				logEvent.SetStackTrace(st, index);
				break;
			}
			_logger.Log(GetType(), logEvent);
		}

		/// <summary>
		/// Commons.Logging中的日誌級別與NLog中的日誌級別對應關係設置
		/// </summary>
		/// <param name="logLevel">Commons.Logging中的日誌級別</param>
		/// <returns>自定義日誌級別</returns>
		/// <remarks>
		/// Commons.Logging
		///		OFF ＞ FATAL ＞ ERROR ＞ WARN ＞ INFO ＞ DEBUG  ＞ TRACE  ＞ ALL
		/// NLog
		///		OFF ＞ FATAL ＞ ERROR ＞ WARN ＞ INFO ＞ DEBUG  ＞   ＞   ＞ TRACE
		/// </remarks>
		private static LogLevel GetLevel(Common.Logging.LogLevel logLevel)
		{
			switch (logLevel)
			{
				case Common.Logging.LogLevel.All:
					return LogLevel.Trace;

				case Common.Logging.LogLevel.Trace:
					return LogLevel.Trace;

				case Common.Logging.LogLevel.Debug:
					return LogLevel.Debug;

				case Common.Logging.LogLevel.Info:
					return LogLevel.Info;

				case Common.Logging.LogLevel.Warn:
					return LogLevel.Warn;

				case Common.Logging.LogLevel.Error:
					return LogLevel.Error;

				case Common.Logging.LogLevel.Fatal:
					return LogLevel.Fatal;

				case Common.Logging.LogLevel.Off:
					return LogLevel.Off;

				default:
					throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, MessageUtil.GetMessage(() => MsgResources.E0038L));
			}
		}
	}
}