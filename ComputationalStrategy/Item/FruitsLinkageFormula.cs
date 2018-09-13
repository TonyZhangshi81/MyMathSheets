using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Item
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
		/// 
		/// </summary>
		public IList<int> Sort { get; set; }

		/// <summary>
		/// 容器的擺放位置（從0開始）
		/// </summary>
		public IList<int> Seats { get; set; }
	}
}
