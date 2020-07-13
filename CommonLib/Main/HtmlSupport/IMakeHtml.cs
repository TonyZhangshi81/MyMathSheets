using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類接口
	/// </summary>
	public interface IMakeHtml
	{
		/// <summary>
		/// 取得HTML模板信息
		/// </summary>
		/// <typeparam name="T">參數類型參數</typeparam>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="parameter">題型參數</param>
		/// <returns></returns>
		Dictionary<SubstituteType, string> GetHtmlStatement<T>(string topicIdentifier, T parameter) where T : ParameterBase;
	}
}