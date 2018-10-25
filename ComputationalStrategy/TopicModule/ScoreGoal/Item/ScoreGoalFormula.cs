using MyMathSheets.CommonLib.Main.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.ScoreGoal.Item
{
	/// <summary>
	/// 射門得分
	/// </summary>
	public class ScoreGoalFormula
	{
		/// <summary>
		/// 球類计算式集合(計算式, 球門位置(0或者1))
		/// </summary>
		public Dictionary<Formula, int> BallsFormulas { get; set; }

		/// <summary>
		/// 球門容器计算式集合（只有兩個球門0或者1使用）
		/// </summary>
		public IList<Formula> GoalsFormulas { get; set; }
	}
}
