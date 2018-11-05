using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.FromProcess.Item
{
	/// <summary>
	/// 題型CHECKBOX的參數類（用於畫面控件自動作成所需要的參數對象）
	/// </summary>
	public class CheckObjectArgument
	{
		/// <summary>
		/// 控件ID
		/// </summary>
		public string ControlId { get; set; }
		/// <summary>
		/// 題型名稱
		/// </summary>
		public LayoutSetting.Preview Preview { get; set; }
		/// <summary>
		/// 默認是否勾選
		/// </summary>
		public bool IsChecked { get; set; }
	}
}
