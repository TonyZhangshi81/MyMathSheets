using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters
{
	/// <summary>
	/// 射門得分參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.ScoreGoal, "SG001|SG002|SG003")]
	public class ScoreGoalParameter : ParameterBase
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
			base.InitParameter();

			// 集合實例化
			Formulas = new ScoreGoalFormula();
		}
	}
}
