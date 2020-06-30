namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IConsoleWrite<T>
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="formulas"></param>
		void ConsoleFormulas(T formulas);
	}
}