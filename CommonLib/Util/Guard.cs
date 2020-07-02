using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="argument"></param>
		/// <param name="parameterName"></param>
		public static void ArgumentNotNull<T>(T argument, string parameterName) where T : class
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(parameterName, MessageUtil.GetException(() => MsgResources.E0036L, parameterName));
			}
			if (argument == null)
			{
				throw new ArgumentNullException(MessageUtil.GetException(() => MsgResources.E0037L));
			}
		}
	}
}
