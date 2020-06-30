using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using System;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// LOG日誌接口類
	/// </summary>
	public interface ILog
	{
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

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		/// <param name="bcp"></param>
		void Debug(string message, Exception exception, CalculateParameter bcp);

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		/// <param name="bcp"></param>
		void Debug(string message, Exception exception, ParameterBase bcp);
	}
}