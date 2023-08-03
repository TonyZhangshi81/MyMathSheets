using System;
using System.Runtime.Serialization;

namespace MyMathSheets.CommonLib.Composition
{
	/// <summary>
	/// MEF容器中未找到定義時的例外
	/// </summary>
	[Serializable]
	public class ComposerException : Exception
	{
		/// <summary>
		/// 初期化新實例
		/// </summary>
		public ComposerException()
			: base()
		{
		}

		/// <summary>
		/// 指定表示當前異常的信息，初始化新實例
		/// </summary>
		/// <param name="message">異常的信息</param>
		public ComposerException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// 使用序列化的數據對新實例進行初始化
		/// </summary>
		/// <param name="info">保持序列化的對象數據的對象</param>
		/// <param name="context">關於轉送方或轉送地的信息</param>
		protected ComposerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// 指定導致當前異常的異常並初始化新實例
		/// </summary>
		/// <param name="message">異常的信息</param>
		/// <param name="innerException">内部例外</param>
		public ComposerException(string message, System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}