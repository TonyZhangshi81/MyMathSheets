﻿using MyMathSheets.CommonLib.Logging;
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
				if (d.Type == (int)TopicType.Multiple)
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

				// 加法和減法巧算
				if (d.Type == (int)TopicType.Plus || d.Type == (int)TopicType.Subtraction)
				{
					string flag = "topic";
					d.ConfixFormulas.ToList().ForEach(dd =>
					{
						if ("topic".Equals(flag))
						{
							Console.Write(string.Format("{0} {1} {2} = ", dd.LeftParameter, dd.Sign.ToOperationString(), dd.RightParameter));
						}
						if ("result".Equals(flag))
						{
							Console.Write(string.Format("{0} {1} {2} = ",
								CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, dd.LeftParameter, CommonLib.Util.GapFilling.Left),
								dd.Sign.ToOperationString(),
								CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, dd.RightParameter, CommonLib.Util.GapFilling.Right)
								));
						}
						flag = "result";
					});
					d.Answer.ForEach(dd =>
					{
						Console.Write(dd);
					});
				}

				// 綜合題型巧算（拆解）
				if (d.Type == (int)Synthetic.Unknit)
				{
					Console.Write(string.Format("{0} {1} {2} = ", d.ConfixFormulas[0].LeftParameter, d.ConfixFormulas[0].Sign.ToOperationString(), d.ConfixFormulas[0].RightParameter));

					Console.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} = ",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.ConfixFormulas[1].LeftParameter, CommonLib.Util.GapFilling.Left),
						d.ConfixFormulas[1].Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.ConfixFormulas[1].RightParameter, CommonLib.Util.GapFilling.Right),

						d.ConfixFormulas[3].Sign.ToOperationString(),

						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.ConfixFormulas[2].LeftParameter, CommonLib.Util.GapFilling.Left),
						d.ConfixFormulas[2].Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.ConfixFormulas[2].RightParameter, CommonLib.Util.GapFilling.Right)
						));

					Console.Write(d.Answer[0]);
				}

				// 綜合題型巧算（合併）
				if (d.Type == (int)Synthetic.Combine)
				{
					Console.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} = ",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.ConfixFormulas[1].LeftParameter, CommonLib.Util.GapFilling.Default),
						d.ConfixFormulas[1].Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.ConfixFormulas[1].RightParameter, CommonLib.Util.GapFilling.Default),

						d.ConfixFormulas[3].Sign.ToOperationString(),

						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.ConfixFormulas[2].LeftParameter, CommonLib.Util.GapFilling.Default),
						d.ConfixFormulas[2].Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.ConfixFormulas[2].RightParameter, CommonLib.Util.GapFilling.Default)
						));

					Console.Write(string.Format("{0} {1} {2} = ",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.ConfixFormulas[0].LeftParameter, CommonLib.Util.GapFilling.Left),
						d.ConfixFormulas[0].Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.ConfixFormulas[0].RightParameter, CommonLib.Util.GapFilling.Right)));

					Console.Write(d.Answer[0]);
				}

				Console.WriteLine();
			});
		}
	}
}