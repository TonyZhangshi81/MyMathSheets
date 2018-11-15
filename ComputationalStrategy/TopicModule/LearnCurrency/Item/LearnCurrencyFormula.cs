using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Item
{
	/// <summary>
	/// 題型計算式對象構成
	/// </summary>
	public class LearnCurrencyFormula
	{
		/// <summary>
		/// 元单位
		/// </summary>
		public int YuanUnit { get; set; }
		/// <summary>
		/// 角单位
		/// </summary>
		public int JiaoUnit { get; set; }
		/// <summary>
		/// 分单位
		/// </summary>
		public int FenUnit { get; set; }
		/// <summary>
		/// 填空項目種類(左邊或者右邊為填空項目)
		/// </summary>
		public GapFilling Gap { get; set; }

		/// <summary>
		/// 剩下的角
		/// </summary>
		public int? RemainderJiao { get; set; }
		/// <summary>
		/// 剩下的角
		/// </summary>
		public int? RemainderFen { get; set; }

		/// <summary>
		/// 貨幣轉換題型種類
		/// </summary>
		public CurrencyTransform CurrencyTransformType { get; set; }
	}
}
