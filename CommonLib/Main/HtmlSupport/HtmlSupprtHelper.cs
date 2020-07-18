using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 題型HTML支援類
	/// </summary>
	public class HtmlSupprtHelper
	{
		private readonly IHtmlSupportFactory HtmlSupportFactory;

		/// <summary>
		/// <see cref="HtmlSupprtHelper" />的構造函數
		/// </summary>
		/// <param name="htmlSupportFactory">題型HTML支援類工廠</param>
		public HtmlSupprtHelper(IHtmlSupportFactory htmlSupportFactory)
		{
			Guard.ArgumentNotNull(htmlSupportFactory, "htmlSupportFactory");

			HtmlSupportFactory = htmlSupportFactory;
		}

		/// <summary>
		/// 對指定HTML支援類實例化
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>運算符實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(string topicIdentifier)
		{
			// 運算符工廠實例化
			return HtmlSupportFactory.CreateHtmlSupportInstance(topicIdentifier);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		public void ReleaseExportsHtmlSupport(string topicIdentifier)
		{
			HtmlSupportFactory.ReleaseExportsHtmlSupport(topicIdentifier);
		}
	}
}