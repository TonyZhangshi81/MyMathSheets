namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 出題策略接口類
	/// </summary>
	public interface ITopic : System.IDisposable
	{
		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="p">參數類</param>
		void Build(TopicParameterBase p);
	}
}