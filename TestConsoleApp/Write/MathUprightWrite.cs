using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 豎式計算題型計算式結果顯示輸出
	/// </summary>
	[TopicWrite("MathUpright")]
	public class MathUprightWrite : TopicWriteBase<MathUprightParameter>
	{
		/// <summary>
		/// 計算式結果顯示輸出
		/// </summary>
		/// <param name="formulas">豎式計算題型計算式</param>
		public void ConsoleFormulas(IList<MathUprightFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "豎式計算"));

			formulas.ToList().ForEach(d =>
			{
				var index = 1;
				d.FormulaDataLink.ForEach(f =>
				{
					// 運算符所在位置
					if (index == 3)
					{
						// 運算符打印
						Console.Write(d.Arithmetic.Sign.ToOperationString());
					}
					if (index == 5)
					{
						Console.Write("=");
					}

					if (f == null)
					{
						Console.Write(string.Empty);
					}
					else
					{
						if (index == (d.FillPosition / 10) || index == (d.FillPosition % 10))
						{
							Console.Write(string.Format("[{0}]", f));
						}
						else
						{
							Console.Write(f);
						}
					}
					index++;
				});

				Console.WriteLine();
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(MathUprightParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}