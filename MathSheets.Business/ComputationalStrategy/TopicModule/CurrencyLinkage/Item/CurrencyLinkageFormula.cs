using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item
{
	/// <summary>
	/// 認識價格
	/// </summary>
	public class CurrencyLinkageFormula
	{
		/// <summary>
		/// 左邊(上面)價格集合
		/// </summary>
		public IList<decimal> LeftCurrencys { get; set; }

		/// <summary>
		/// 右邊(下面)價格集合
		/// </summary>
		public IList<int> RightCurrencys { get; set; }

		/// <summary>
		/// 隨機排序
		/// </summary>
		public IList<int> Sort { get; set; }

		/// <summary>
		/// 容器的擺放位置（從0開始）
		/// </summary>
		public IList<int> Seats { get; set; }
	}
}