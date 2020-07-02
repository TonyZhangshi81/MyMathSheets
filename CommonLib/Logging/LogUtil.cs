using Common.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	///
	/// </summary>
	public static class LogUtil
	{
		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="logs">日誌實例</param>
		/// <param name="createContext">日誌信息</param>
		private static void LogDebug(IEnumerable<Common.Logging.ILog> logs, Func<LogContext> createContext)
		{
			LogDebug(logs, () => createContext(), null);
		}

		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="logs">日誌實例</param>
		/// <param name="createContext">日誌信息</param>
		/// <param name="exception">異常</param>
		private static void LogDebug(IEnumerable<Common.Logging.ILog> logs, Func<LogContext> createContext, Exception exception)
		{
			LogContext context = null;

			foreach (var log in logs)
			{
				if (log.IsDebugEnabled)
				{
					if (context == null)
					{
						context = createContext();
					}
					log.Debug(context, exception);
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private static string BuildArguments(Dictionary<string, string> args)
		{
			var line = new StringBuilder();
			foreach (var arg in args)
			{
				line.AppendFormat("<{0}> {1}; ", arg.Key, arg.Value);
			}
			return line.ToString();
		}

		#region Calculate

		private const string CalculateEventContextItemName = "Formula";

		/// <summary>
		/// 計算式日誌處理
		/// </summary>
		private static readonly IList<Common.Logging.ILog> CalculateLoggings = new List<Common.Logging.ILog>() { LogManager.GetLogger("Calculate") };

		/// <summary>
		/// 日誌處理：計算式作成處理
		/// </summary>
		/// <param name="formula">计算式</param>
		public static void LogCalculate(Formula formula)
		{
			LogDebug(
				CalculateLoggings,
				() =>
					{
						var dump = new StringBuilder();
						dump.Append(formula.LeftParameter).Append(" ");
						dump.Append(formula.Sign.ToOperationString()).Append(" ");
						dump.Append(formula.RightParameter).Append(" ");
						dump.Append(SignOfCompare.Equal.ToSignOfCompareString()).Append(" ");
						dump.Append(formula.Answer);

						var context = new LogContext("Formula");
						context.Properties.Add(CalculateEventContextItemName, dump);
						return context;
					});
		}

		#endregion Calculate

		#region Debug

		internal const string DebugDumpEventContextItemName = "Dump";

		/// <summary>
		/// 調試日誌輸出
		/// </summary>
		private static readonly IList<Common.Logging.ILog> DebugLoggings = new List<Common.Logging.ILog> { LogManager.GetLogger("Debug") };

		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="message">日誌信息</param>
		public static void LogDebug(string message)
		{
			LogDebug(DebugLoggings,
				() =>
				{
					var context = new LogContext(message);
					return context;
				},
				null);
		}

		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="message">日誌信息</param>
		/// <param name="args">參數</param>
		public static void LogDebug(string message, Dictionary<string, string> args)
		{
			LogDebug(DebugLoggings,
				() =>
				{
					var context = new LogContext(message);
					if (args != null)
					{
						context.Properties.Add(DebugDumpEventContextItemName, BuildArguments(args));
					}
					return context;
				},
				null);
		}

		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="message">日誌信息</param>
		/// <param name="exception">異常對象</param>
		/// <param name="args">參數</param>
		public static void LogDebug(string message, Exception exception, Dictionary<string, string> args)
		{
			LogDebug(DebugLoggings,
				() =>
				{
					var context = new LogContext(message);
					if (args != null)
					{
						context.Properties.Add(DebugDumpEventContextItemName, BuildArguments(args));
					}
					return context;
				},
				exception);
		}

		#endregion Debug

		#region Error

		/// <summary>
		/// 日誌輸出
		/// </summary>
		/// <param name="message">日誌信息</param>
		/// <param name="exception">異常對象</param>
		public static void LogError(string message, Exception exception)
		{
			Log log = (Log)Log.Instance;
			log.Output(message, MessageLevel.Error, exception);
		}

		#endregion Error
	}
}