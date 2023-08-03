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

		/// <summary>
		/// 返回指定單元格的公式
		/// </summary>
		/// <param name="cellName">單元格位置</param>
		/// <returns>單元格的公式</returns>
		string GetFormula(string cellName);
	}
}