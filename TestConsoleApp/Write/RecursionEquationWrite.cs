using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Strategy;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using MyMathSheets.TestConsoleApp.Write.Attributes;
using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 遞等式計算题型计算式结果显示输出
	/// </summary>
	[TopicWrite("RecursionEquation")]
	public class RecursionEquationWrite : TopicWriteBase<RecursionEquationParameter>
	{
		/// <summary>
		/// 計算結果顯示輸出
		/// </summary>
		/// <param name="parameter">參數</param>
		public override void ConsoleFormulas(RecursionEquationParameter parameter)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "遞等式計算"));

			parameter.Formulas.ToList().ForEach(d =>
			{
				if (d.Type == TopicType.CleverA
						|| d.Type == TopicType.CleverG
						|| d.Type == TopicType.CleverH)
				{
					// CleverA: 22+44+56 -> 22+(44+56)
					//			列1: 22+44 = 66
					//			列2: 66+56 = 122
					// CleverG: 68-44+12 -> 68+12-44
					//			列1: 68-44 = 24
					//			列2: 24+12 = 36
					// CleverH: 68-44-18 -> 68-18-44
					//			列1: 68-44 = 24
					//			列2: 24-18 = 6
					Console.Write(string.Format("{0} {1} {2} {3} {4} = {5}",
						d.ConfixFormulas[0].LeftParameter,
						d.ConfixFormulas[0].Sign.ToOperationString(),
						d.ConfixFormulas[0].RightParameter,
						d.ConfixFormulas[1].Sign.ToOperationString(),
						d.ConfixFormulas[1].RightParameter,
						d.Answer[0]));
				}
				else if (d.Type == TopicType.CleverB
						|| d.Type == TopicType.CleverC
						|| d.Type == TopicType.CleverD
						|| d.Type == TopicType.CleverE
						|| d.Type == TopicType.CleverF)
				{
					// CleverB: 68-(44-12) -> 68-44+12 -> 68+12-44
					//			列1: 44-12 = 32
					//			列2: 68-32 = 36
					// CleverC: 62-(44+12) -> 62-44-12 -> 62-12-44
					//			列1: 44+12 = 56
					//			列2: 62-56 = 6
					// CleverD: 62+(44-12) -> 62+44-12 -> 62-12+44
					//			列1: 44-12 = 32
					//			列2: 62+32 = 94
					// CleverE: 68+(44+12) -> 68+44+12 -> 68+12+44
					//			列1: 44+12 = 56
					//			列2: 68+56 = 124
					// CleverF: 68+44-18 -> 68-18+44
					//			列1: 68+44 = 112
					//			列2: 112-18 = 94
					Console.Write(string.Format("{0} {1} ({2} {3} {4}) = {5}",
						d.ConfixFormulas[1].LeftParameter,
						d.ConfixFormulas[1].Sign.ToOperationString(),
						d.ConfixFormulas[0].LeftParameter,
						d.ConfixFormulas[0].Sign.ToOperationString(),
						d.ConfixFormulas[0].RightParameter,
						d.Answer[0]));
				}

				Console.WriteLine();
			});
		}
	}
}