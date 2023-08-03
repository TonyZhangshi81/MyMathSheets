using MyMathSheets.CommonLib.Util;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace MyMathSheets.CommonLib.Message
{
	/// <summary>
	/// 信息內容取得(內容取自資源文件)
	/// </summary>
	public static class MessageUtil
	{
		/// <summary>
		/// 信息內容取得
		/// </summary>
		/// <param name="messageKeyExpression">消息KEY</param>
		/// <param name="args">消息中的參數信息<see cref="object"/>數組</param>
		/// <returns>完整的消息內容</returns>
		/// <exception cref="ArgumentNullException"><paramref name="messageKeyExpression"/>為NULL的情況</exception>
		public static string GetMessage(Expression<Func<string>> messageKeyExpression, params object[] args)
		{
			Guard.ArgumentNotNull(messageKeyExpression, "messageKeyExpression");

			var message = messageKeyExpression.Compile()();
			var formattedMessage = string.Format(CultureInfo.CurrentCulture, message, args.Select(_ => _ ?? string.Empty).ToArray());
			return formattedMessage;
		}
	}
}