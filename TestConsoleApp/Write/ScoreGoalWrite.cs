using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Item;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 射門得分题型计算式结果显示输出
	/// </summary>
	public class ScoreGoalWrite : IConsoleWrite<ScoreGoalFormula>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(ScoreGoalFormula formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "射門得分"));

			int seat = 0;
			// 信息打印
			formulas.GoalsFormulas.ToList().ForEach(d =>
			{
				// 球門信息顯示
				Console.WriteLine(string.Format(" 球門{0}  {1} {2} {3} = {4}",
									seat,
									CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, d.LeftParameter, d.Gap),
									d.Sign.ToOperationString(),
									CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, d.RightParameter, d.Gap),
									CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, d.Answer, d.Gap)));
				// 該球門內的足球信息顯示
				formulas.BallsFormulas.ToList().ForEach(m =>
				{
					if (m.Value == seat)
					{
						Console.WriteLine(string.Format(" 足球  {0} {1} {2} = {3}",
											CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, m.Key.LeftParameter, m.Key.Gap),
											m.Key.Sign.ToOperationString(),
											CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, m.Key.RightParameter, m.Key.Gap),
											CommonUtil.GetValue(CommonLib.Util.GapFilling.Default, m.Key.Answer, m.Key.Gap)));
					}
				});
				seat++;
			});
		}
	}
}