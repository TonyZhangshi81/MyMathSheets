using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 射門得分調試信息打印
	/// </summary>
	public class ScoreGoalWrite : IConsoleWrite<ScoreGoalFormula>
	{
		/// <summary>
		/// 測試并顯示結果
		/// </summary>
		/// <param name="formulas">等式集合</param>
		public void ConsoleFormulas(ScoreGoalFormula formulas)
		{
			int seat = 0;
			// 信息打印
			formulas.GoalsFormulas.ToList().ForEach(d =>
			{
				// 球門信息顯示
				Console.WriteLine(string.Format(" 球門{0}  {1} {2} {3} = {4}",
									seat,
									Util.GetValue(GapFilling.Default, d.LeftParameter, d.Gap),
									d.Sign.ToOperationString(),
									Util.GetValue(GapFilling.Default, d.RightParameter, d.Gap),
									Util.GetValue(GapFilling.Default, d.Answer, d.Gap)));
				// 該球門內的足球信息顯示
				formulas.BallsFormulas.ToList().ForEach(m =>
				{
					if (m.Value == seat)
					{
						Console.WriteLine(string.Format(" 足球  {0} {1} {2} = {3}",
											Util.GetValue(GapFilling.Default, m.Key.LeftParameter, m.Key.Gap),
											m.Key.Sign.ToOperationString(),
											Util.GetValue(GapFilling.Default, m.Key.RightParameter, m.Key.Gap),
											Util.GetValue(GapFilling.Default, m.Key.Answer, m.Key.Gap)));
					}
				});
				seat++;
			});
		}
	}
}
