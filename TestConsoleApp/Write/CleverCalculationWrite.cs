using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Strategy;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Globalization;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 巧算题型计算式结果显示输出
	/// </summary>
	[TopicWrite("CleverCalculation")]
	public class CleverCalculationWrite : TopicWriteBase<CleverCalculationParameter>
	{
		/// <summary>
		/// 計算結果顯示輸出
		/// </summary>
		/// <param name="parameter">參數</param>
		public override void ConsoleFormulas(CleverCalculationParameter parameter)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "巧算"));

			parameter.Formulas.ToList().ForEach(d =>
			{
				// 乘法巧算
				if (d.Type == TopicType.Multiple)
				{
					Console.Write(d.ConfixFormulas[0].Answer);
					d.ConfixFormulas.ToList().ForEach(dd =>
					{
						Console.Write(string.Format(" = {0} {1} {2}",
							CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, dd.LeftParameter, dd.Gap),
							dd.Sign.ToOperationString(),
							CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, dd.RightParameter, dd.Gap)
							));
					});

					Console.WriteLine();
					Console.Write("答案:");
					d.Answer.ForEach(dd =>
					{
						Console.Write(string.Format("{0},", dd, CultureInfo.CurrentCulture));
					});
				}

				// 加法巧算
				if (d.Type == TopicType.Plus)
				{
					string flag = "topic";
					d.ConfixFormulas.ToList().ForEach(dd =>
					{
						if ("topic".Equals(flag))
						{
							Console.Write(string.Format("{0} {1} {2} = ", dd.LeftParameter, dd.Sign.ToOperationString(), dd.RightParameter));
							flag = "result";
						}
						if ("result".Equals(flag))
						{
							Console.Write(string.Format("{0} {1} {2} = ",
								CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, dd.LeftParameter, CommonLib.Util.GapFilling.Left),
								dd.Sign.ToOperationString(),
								CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, dd.RightParameter, CommonLib.Util.GapFilling.Right)
								));
						}
					});
					d.Answer.ForEach(dd =>
					{
						Console.Write(dd);
					});
				}

				Console.WriteLine();
			});
		}
	}
}