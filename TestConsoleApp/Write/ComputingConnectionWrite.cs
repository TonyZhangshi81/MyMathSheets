using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Item;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 等式接龍题型计算式结果显示输出
	/// </summary>
	public class ComputingConnectionWrite : IConsoleWrite<List<ConnectionFormula>>
	{
		private static Log log = Log.LogReady(typeof(ComputingConnectionWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<ConnectionFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "等式接龍"));

			StringBuilder builder = new StringBuilder();
			formulas.ToList().ForEach(d =>
			{
				builder.Length = 0;
				d.ConfixFormulas.ToList().ForEach(dd =>
				{
					builder.AppendFormat("{0} {1} {2} = ",
										Util.GetValue(GapFilling.Left, dd.LeftParameter, dd.Gap),
										dd.Sign.ToOperationString(),
										Util.GetValue(GapFilling.Right, dd.RightParameter, dd.Gap));
				});
				if (builder.Length != 0)
				{
					// 最有一层需要把计算结果显示出来
					builder.Append(d.ConfixFormulas[d.ConfixNumber - 1].Answer);
				}
				Console.WriteLine(builder.ToString());
			});

			builder = null;
		}
	}
}
