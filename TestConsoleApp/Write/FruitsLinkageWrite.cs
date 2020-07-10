using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Item;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 水果連連看题型计算式结果显示输出
	/// </summary>
	public class FruitsLinkageWrite : IConsoleWrite
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(FruitsLinkageFormula formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "水果連連看"));

			int index = 0;
			formulas.FruitsFormulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("水果：{0} {1} {2} = {3}   容器編號：{4}",
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.RightParameter, d.Gap),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Answer, d.Gap),
					formulas.Seats[index++]));
			});

			int seat = 0;
			formulas.Sort.ToList().ForEach(d =>
			{
				Formula container = formulas.ContainersFormulas[d];

				CommonLib.Util.GapFilling gap = container.Gap;
				Console.WriteLine(string.Format("容器{0}：{1} {2} {3} = {4}",
					seat++,
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, container.LeftParameter, gap),
					container.Sign.ToOperationString(),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, container.RightParameter, gap),
					CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, container.Answer, gap)));
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public void ConsoleFormulas(ParameterBase parameter)
		{
			FruitsLinkageParameter param = (FruitsLinkageParameter)parameter;

			ConsoleFormulas(param.Formulas);
		}
	}
}