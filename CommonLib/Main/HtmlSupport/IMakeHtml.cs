using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;

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
		/// <param name="supportType">HTML支援類對象類型</param>
		/// <returns></returns>
		string GetHtmlStatement<T>(LayoutSetting.Preview preview, T formulas, out Type supportType) where T : ParameterBase;
	}
}
