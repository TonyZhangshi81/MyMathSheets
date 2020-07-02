using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.FromProcess.Support
{
	/// <summary>
	/// 控件基本信息
	/// </summary>
	public class ControlInfo
	{
		/// <summary>
		/// 坐標X
		/// </summary>
		public int IndexX { get; set; }

		/// <summary>
		/// 坐標Y
		/// </summary>
		public int IndexY { get; set; }

		/// <summary>
		/// 控件ID
		/// </summary>
		public string ControlId { get; set; }

		/// <summary>
		/// 控件標題
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 題型分類
		/// </summary>
		public LayoutSetting.Classify Classify { get; set; }

		/// <summary>
		/// 題型類型
		/// </summary>
		public LayoutSetting.Preview Preview { get; set; }
	}
}