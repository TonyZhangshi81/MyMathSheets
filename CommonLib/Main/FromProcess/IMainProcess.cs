using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.FromProcess
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMainProcess
	{
		/// <summary>
		/// 控件基本屬性取得
		/// </summary>
		List<ControlInfo> ControlList { get; }

		/// <summary>
		/// 題型瀏覽內容列表
		/// </summary>
		List<LayoutSetting.Preview> LayoutSettingPreviewList { get; set; }

		/// <summary>
		/// 出題按鍵點擊事件
		/// </summary>
		/// <returns></returns>
		string SureClick();

		/// <summary>
		/// 題型選擇校驗
		/// </summary>
		/// <returns></returns>
		bool ChooseCheck();

		/// <summary>
		/// 題型預覽列表設置
		/// </summary>
		/// <param name="name">題型名稱</param>
		void SetLayoutSettingPreviewList(LayoutSetting.Preview name);

		/// <summary>
		/// 題型項目選擇事件
		/// </summary>
		/// <param name="isChecked">是否選擇</param>
		/// <param name="controlId">控件ID</param>
		void TopicCheckedChanged(bool isChecked, string controlId);
	}
}
