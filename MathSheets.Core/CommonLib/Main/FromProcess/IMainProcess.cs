using System.IO;

namespace MyMathSheets.CommonLib.Main.FromProcess
{
	/// <summary>
	/// 主要處理
	/// </summary>
	public interface IMainProcess
	{
		/// <summary>
		/// 出題實施
		/// </summary>
		/// <returns>頁面文件對象</returns>
		FileInfo Compile();
	}
}