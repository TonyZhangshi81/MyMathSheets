using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類實例生產工廠
	/// </summary>
	public interface IHtmlSupportFactory
	{
		/// <summary>
		/// 題型指定獲取HTML支援類實例
		/// </summary>
		/// <param name="preview">題型類型</param>
		/// <returns>HTML支援類實例</returns>
		IHtmlSupport CreateHtmlSupportInstance(string preview);
	}
}