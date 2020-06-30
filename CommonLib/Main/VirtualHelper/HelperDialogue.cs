using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.VirtualHelper
{
	/// <summary>
	/// 會話內容對象
	/// </summary>
	public class HelperDialogue
	{
		/// <summary>
		/// 會話內容列表
		/// </summary>
		public List<string> Dialogues { get; set; }

		/// <summary>
		/// 會話內容所對應題目的序號（即位置）
		/// </summary>
		public List<int> FormulaIndex { get; set; }
	}
}