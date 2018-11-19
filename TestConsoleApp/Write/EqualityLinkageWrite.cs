using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 算式連一連题型计算式结果显示输出
	/// </summary>
	public class EqualityLinkageWrite : IConsoleWrite<EqualityLinkageFormula>
	{
		private static Log log = Log.LogReady(typeof(EqualityLinkageWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(EqualityLinkageFormula formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "算式連一連"));

			int index = 0;
			formulas.LeftFormulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("左邊：{0} {1} {2} = {3}   容器編號：{4}",
					Util.GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Answer, d.Answer, d.Gap),
					formulas.Seats[index++]));
			});

			int seat = 0;
			formulas.Sort.ToList().ForEach(d =>
			{
				Formula container = formulas.RightFormulas[d];

				GapFilling gap = container.Gap;
				Console.WriteLine(string.Format("右邊{0}：{1} {2} {3} = {4}",
					seat++,
					Util.GetValue(GapFilling.Left, container.LeftParameter, gap),
					container.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, container.RightParameter, gap),
					Util.GetValue(GapFilling.Answer, container.Answer, gap)));
			});
		}
	}
}
