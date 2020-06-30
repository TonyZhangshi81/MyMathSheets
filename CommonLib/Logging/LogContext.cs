using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	///
	/// </summary>
	public class LogContext : ILogContext
	{
		/// <summary>
		///
		/// </summary>
		public object Message
		{
			get;
			private set;
		}

		/// <summary>
		///
		/// </summary>
		public IDictionary<object, object> Properties
		{
			get;
			set;
		}

		/// <summary>
		///
		/// </summary>
		public LogContext(object message)
		{
			Message = message;
			Properties = new Dictionary<object, object>();
		}
	}
}