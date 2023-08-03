namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 題型實例工廠接口類
	/// </summary>
	public interface ITopicFactory
	{
		/// <summary>
		/// 指定題型識別ID並返回該題型的實例
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>題型實例</returns>
		ITopic CreateTopicInstance(string topicIdentifier);

		/// <summary>
		/// 返回指定題型所需的題型參數實例
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="topicNumber">題型參數（如果沒有指定題型參數，則默認返回當前參數序列的第一個參數項目）</param>
		/// <returns>題型參數實例</returns>
		TopicParameterBase CreateTopicParameterInstance(string topicIdentifier, string topicNumber);

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		void Release(string topicIdentifier);
	}
}