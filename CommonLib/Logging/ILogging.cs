using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// LOG日誌接口類
	/// </summary>
	public interface ILogging
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		void Output(string message);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		void Output(string message, Exception exception);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		void Debug(string message);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		void Debug(string message, Exception exception);
	}
}
