using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	///
	/// </summary>
	public interface ILogContext
	{
		/// <summary>
		///
		/// </summary>
		object Message
		{
			get;
		}

		/// <summary>
		///
		/// </summary>
		IDictionary<object, object> Properties
		{
			get;
		}
	}
}