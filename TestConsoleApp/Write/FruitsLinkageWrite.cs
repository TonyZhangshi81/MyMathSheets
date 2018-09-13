using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 
	/// </summary>
	public class FruitsLinkageWrite : IConsoleWrite<FruitsLinkageFormula>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(FruitsLinkageFormula formulas)
		{
			int index = 0;
			formulas.FruitsFormulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("水果：{0} {1} {2} = {3}   容器編號：{4}",
					Util.GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Answer, d.Answer, d.Gap),
					formulas.Seats[index++]));
			});

			int seat = 0;
			formulas.Sort.ToList().ForEach(d =>
			{
				Formula container = formulas.ContainersFormulas[d];

				GapFilling gap = container.Gap;
				Console.WriteLine(string.Format("容器{0}：{1} {2} {3} = {4}",
					seat++,
					Util.GetValue(GapFilling.Left, container.LeftParameter, gap),
					container.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, container.RightParameter, gap),
					Util.GetValue(GapFilling.Answer, container.Answer, gap)));
			});
		}
	}
}
