using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using System.Collections.Concurrent;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// 題型HTML支援接口
	/// </summary>
	public interface IHtmlSupport<in T>
		where T : TopicParameterBase
	{
		/// <summary>
		/// HTML上下文內容作成並返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML上下文內容</returns>
		string MakeHtmlContent(T parameter);
	}

	/// <summary>
	/// 題型HTML支援接口
	/// </summary>
	public interface IHtmlSupport
	{
		/// <summary>
		/// HTML模板屬性信息採集並返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板屬性信息集合</returns>
		ConcurrentDictionary<SubstituteType, string> MakeHtmlMaps(TopicParameterBase parameter);
	}
}