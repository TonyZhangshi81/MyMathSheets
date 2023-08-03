using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.FruitsLinkage.Item
{
	/// <summary>
	/// 水果連連看
	/// </summary>
	public class FruitsLinkageFormula
	{
		/// <summary>
		/// 左邊水果计算式集合
		/// </summary>
		public IList<Formula> FruitsFormulas { get; set; }

		/// <summary>
		/// 右邊容器计算式集合
		/// </summary>
		public IList<Formula> ContainersFormulas { get; set; }

		/// <summary>
		/// 各計算式編號的集合并隨機排序
		/// </summary>
		public IList<int> Sort { get; set; }

		/// <summary>
		/// 左邊計算式->右側計算式 擺放位置（從0開始）
		/// </summary>
		public IList<int> Seats { get; set; }
	}
}