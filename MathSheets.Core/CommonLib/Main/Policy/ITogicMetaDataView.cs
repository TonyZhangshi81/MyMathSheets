namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 運算符自定導出的元數據特性（使用其導出的元數據以挑選需要的對象）
	/// </summary>
	public interface ITogicMetaDataView
	{
		/// <summary>
		/// 題型識別
		/// </summary>
		string TopicIdentifier { get; }
	}
}