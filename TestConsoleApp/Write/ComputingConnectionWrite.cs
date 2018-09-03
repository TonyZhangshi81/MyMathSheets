using CommonLib.Util;
using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Write
{
	/// <summary>
	/// 
	/// </summary>
	public class ComputingConnectionWrite : IConsoleWrite<List<ConnectionFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		public void ConsoleFormulas(List<ConnectionFormula> formulas)
		{
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
		}
	}
}
