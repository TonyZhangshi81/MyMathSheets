using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;
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
		/// <param name="preview">題型類型</param>
		/// <param name="formulas">題型參數對象</param>
		/// <returns></returns>
		Dictionary<string, string> GetHtmlStatement<T>(LayoutSetting.Preview preview, T formulas) where T : ParameterBase;
	}
}
