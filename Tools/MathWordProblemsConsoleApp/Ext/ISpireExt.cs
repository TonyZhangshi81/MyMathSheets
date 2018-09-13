namespace MyMathSheets.MathWordProblemsConsoleApp.Ext
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISpireExt
	{
		/// <summary>
		/// Load an existing Excel file cannot be loaded by the password protected file
		/// </summary>
		/// <param name="fileName">The file name</param>
		void Load(string fileName);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellName"></param>
		string GetRangeText(string cellName);
	}
}
