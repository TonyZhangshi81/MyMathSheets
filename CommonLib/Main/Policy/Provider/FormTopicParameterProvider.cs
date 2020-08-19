using MyMathSheets.CommonLib.Main.Policy;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 取得計算式初期參數的配置信息（從畫面中取得參數值）
	/// </summary>
	[TopicParameterProvider("form")]
	public class FormTopicParameterProvider : TopicParameterProvider
	{
		/// <summary>
		/// 通用參數初始化
		/// </summary>
		/// <param name="identifier">題型識別</param>
		/// <param name="replenishArgument">補充參數</param>
		public override TopicParameterBase Initialize(string identifier, out Dictionary<string, string> replenishArgument)
		{
			replenishArgument = null;
			return new TopicParameterBase();
		}
	}
}