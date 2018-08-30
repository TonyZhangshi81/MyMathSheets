namespace TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IMakeHtml<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		string MakeHtml(T formulas);
	}
}
