using MyMathSheets.CommonLib.Main.Policy;

namespace MyMathSheets.TestConsoleApp.Write.Main
{
	/// <summary>
	///
	/// </summary>
	public interface IConsoleWrite<T>
		where T : TopicParameterBase
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		void ConsoleFormulas(T parameter);
	}

	/// <summary>
	///
	/// </summary>
	public interface IConsoleWrite
	{
		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="p">參數類</param>
		void Do(TopicParameterBase p);
	}
}