namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類實例工廠
	/// </summary>
	public interface IHtmlSupportFactory
	{
		/// <summary>
		/// 返回指定題型識別ID的HTML支援類實例
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>HTML支援類實例</returns>
		IHtmlSupport CreateHtmlSupportInstance(string topicIdentifier);

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		void ReleaseExportsHtmlSupport(string topicIdentifier);
	}
}