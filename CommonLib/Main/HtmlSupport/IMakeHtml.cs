using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMakeHtml
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="preview"></param>
		/// <param name="formulas"></param>
		/// <param name="supportType"></param>
		/// <returns></returns>
		string GetHtmlStatement<T>(LayoutSetting.Preview preview, T formulas, out Type supportType) where T : ParameterBase;
	}
}
