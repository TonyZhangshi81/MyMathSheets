using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 找規律题型计算式结果显示输出
	/// </summary>
	[TopicWrite("FindTheLaw")]
	public class FindTheLawFormulaWrite : TopicWriteBase<FindTheLawParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">找規律题型计算式</param>
		public void ConsoleFormulas(IList<FindTheLawFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "找規律"));

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

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(FindTheLawParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}