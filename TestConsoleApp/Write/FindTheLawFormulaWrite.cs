using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 找規律题型计算式结果显示输出
	/// </summary>
	public class FindTheLawFormulaWrite : IConsoleWrite<List<FindTheLawFormula>>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">找規律题型计算式</param>
		public void ConsoleFormulas(List<FindTheLawFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0004T, "找規律"));

			StringBuilder builder = new StringBuilder();
			formulas.ToList().ForEach(d =>
			{
				int index = 0;
				builder.Length = 0;
				d.NumberList.ForEach(f =>
				{
					if (d.RandomIndexList.Any(_ => _ == index))
					{
						builder.AppendFormat("({0}),", f);
					}
					else
					{
						builder.AppendFormat("{0},", f);
					}
					index++;
				});
				builder.Length -= 1;
				Console.WriteLine(builder.ToString());
			});
		}
	}
}