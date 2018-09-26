using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Item
{
	/// <summary>
	/// 射門得分
	/// </summary>
	public class ScoreGoalFormula
	{
		/// <summary>
		/// 球類计算式集合
		/// </summary>
		public IList<Formula> FruitsFormulas { get; set; }

		/// <summary>
		/// 球門容器计算式集合（只有兩個球門0或者1使用）
		/// </summary>
		public IList<Formula> ContainersFormulas { get; set; }

		/// <summary>
		/// </summary>
		public IList<int> Seats { get; set; }
	}
}
