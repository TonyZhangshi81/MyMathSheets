namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 運算符自定導出的元數據特性（使用其導出的元數據以挑選需要的對象）
	/// </summary>
	public interface IOperationMetaDataView
	{
		/// <summary>
		/// 題型類別
		/// </summary>
		string Layout { get; }
	}
}