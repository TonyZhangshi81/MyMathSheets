using MyMathSheets.CommonLib.Main.Policy;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 題型參數供應類
	/// </summary>
	public abstract class TopicParameterProvider
	{
		/// <summary>
		/// 設定參數
		/// </summary>
		public object Argument { get; set; }

		/// <summary>
		/// 參數補充
		/// </summary>
		public Dictionary<string, Dictionary<string, string>> ReplenishArgument { get; set; }

		/// <summary>
		/// 通用題型參數初期化處理
		/// </summary>
		/// <param name="identifier">題型識別ID</param>
		/// <param name="replenishArgument">參數補充</param>
		/// <returns>題型參數</returns>
		public abstract TopicParameterBase Initialize(string identifier, out Dictionary<string, string> replenishArgument);
	}
}