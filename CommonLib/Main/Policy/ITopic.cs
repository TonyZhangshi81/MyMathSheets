namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 出題策略接口類
	/// </summary>
	public interface ITopic<in T>
		where T : TopicParameterBase
	{
		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="p">參數類</param>
		void MarkFormulaList(T p);
	}

	/// <summary>
	/// 出題策略接口類
	/// </summary>
	public interface ITopic
	{
		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="p">參數類</param>
		void Build(TopicParameterBase p);
	}
}