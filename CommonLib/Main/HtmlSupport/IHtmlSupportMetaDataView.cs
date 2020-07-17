namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類自定導出的元數據特性（使用其導出的元數據以挑選需要的對象）
	/// </summary>
	public interface IHtmlSupportMetaDataView
	{
		/// <summary>
		/// 題型類別
		/// </summary>
		string Layout { get; }
	}
}