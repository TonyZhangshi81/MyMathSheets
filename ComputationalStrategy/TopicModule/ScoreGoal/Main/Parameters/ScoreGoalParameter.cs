using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Item;

namespace MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Parameters
{
	/// <summary>
	/// 射門得分參數類
	/// </summary>
	[OperationParameter("ScoreGoal")]
	public class ScoreGoalParameter : TopicParameterBase
	{
		/// <summary>
		/// 射門得分作成并輸出
		/// </summary>
		public ScoreGoalFormula Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 集合實例化
			Formulas = new ScoreGoalFormula();
		}
	}
}