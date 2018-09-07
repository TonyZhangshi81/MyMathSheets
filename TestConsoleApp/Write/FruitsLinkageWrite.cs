using CommonLib.Util;
using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.Write
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
			formulas.FruitsFormulas.ToList().ForEach(d => {

				Formula container = formulas.ContainersFormulas[index];

				Console.WriteLine(string.Format("水果：{0} {1} {2} = {3}  容器編號：{4}   容器{5}：{6} {7} {8} = {9}",
					Util.GetValue(GapFilling.Default, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Default, d.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Default, d.Answer, d.Gap),

					formulas.Seats[index],

					index,

					Util.GetValue(GapFilling.Default, container.LeftParameter, d.Gap),
					container.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Default, container.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Default, container.Answer, d.Gap)
					));

				index++;
			});
		}
	}
}
