using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.VirtualHelper
{
	/// <summary>
	/// 虛擬會話作成接口
	/// </summary>
	public interface IVirtualHelper<T>
	{
		/// <summary>
		/// 對應指定題型數據類型作成幫助提示
		/// </summary>
		/// <param name="formulas">答題集合</param>
		/// <returns>智能提示對象</returns>
		HelperDialogue CreateHelperDialogue(IList<T> formulas);
	}
}