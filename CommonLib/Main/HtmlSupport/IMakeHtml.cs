using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using System.Collections.Concurrent;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類接口
	/// </summary>
	public interface IMakeHtml
	{
		/// <summary>
		/// 取得各題型HTML支援類模板屬性信息集合
		/// </summary>
		/// <typeparam name="T">參數類型參數</typeparam>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="parameter">題型參數</param>
		/// <returns>HTML模板屬性信息集合</returns>
		ConcurrentDictionary<SubstituteType, string> GetHtmlReplaceTagDict<T>(string topicIdentifier, T parameter) where T : TopicParameterBase;

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		void ReleaseExportsHtmlSupport(string topicIdentifier);
	}
}