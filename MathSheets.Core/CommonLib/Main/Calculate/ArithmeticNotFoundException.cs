using System;
using System.Runtime.Serialization;

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	/// CalculateFactory中指定的計算式對象未發現而產生的特定異常
	/// </summary>
	[Serializable]
	public class ArithmeticNotFoundException : Exception
	{
		/// <summary>
		/// 實例初期化
		/// </summary>
		public ArithmeticNotFoundException()
			: base()
		{
		}

		/// <summary>
		/// 指定異常消息為參數的實例初期化
		/// </summary>
		/// <param name="message">異常消息</param>
		public ArithmeticNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// 指定序列化數據為參數的實例初期化
		/// </summary>
		/// <param name="info">可序列化的數據對象</param>
		/// <param name="context">內容</param>
		protected ArithmeticNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// 指定異常消息和異常對象的實例初期化
		/// </summary>
		/// <param name="message">異常消息</param>
		/// <param name="innerException">內部異常對象</param>
		public ArithmeticNotFoundException(string message, System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}