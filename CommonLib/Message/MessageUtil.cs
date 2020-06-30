using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyMathSheets.CommonLib.Message
{
	/// <summary>
	///
	/// </summary>
	public static class MessageUtil
	{
		/// <summary>
		/// リソースメッセージを例外スロー用に書式加工して取得します。
		/// </summary>
		/// <param name="messageKeyExpression">メッセージキーを表す式、nullであってはいけません。</param>
		/// <param name="args">0個以上の書式設定対象オブジェクトを含んだ<see cref="object"/>>配列。</param>
		/// <returns>フォーマットされたメッセージ。</returns>
		/// <exception cref="ArgumentNullException"><paramref name="messageKeyExpression"/>がnullの場合。</exception>
		public static string GetException(Expression<Func<string>> messageKeyExpression, params object[] args)
		{
			var message = messageKeyExpression.Compile()();
			var formattedMessage = string.Format(message, args.Select(_ => _ ?? string.Empty).ToArray());
			return formattedMessage;
		}
	}
}