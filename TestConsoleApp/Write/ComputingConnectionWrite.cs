using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 等式接龍题型计算式结果显示输出
	/// </summary>
	public class ComputingConnectionWrite : IConsoleWrite
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(ParameterBase parameter)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "等式接龍"));

			ComputingConnectionParameter param = (ComputingConnectionParameter)parameter;

			StringBuilder builder = new StringBuilder();
			param.Formulas.ToList().ForEach(d =>
			{
				builder.Length = 0;
				d.ConfixFormulas.ToList().ForEach(dd =>
				{
					builder.AppendFormat("{0} {1} {2} = ",
										CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, dd.LeftParameter, dd.Gap),
										dd.Sign.ToOperationString(),
										CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, dd.RightParameter, dd.Gap));
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