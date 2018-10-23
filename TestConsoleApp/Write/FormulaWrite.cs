using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 四則運算题型计算式结果显示输出
	/// </summary>
	public class FormulaWrite : IConsoleWrite<List<Formula>>
	{
		private static Log log = Log.LogReady(typeof(FormulaWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<Formula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "四則運算"));

			formulas.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} {1} {2} = {3}",
					Util.GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					d.Sign.ToOperationString(),
					Util.GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					Util.GetValue(GapFilling.Answer, d.Answer, d.Gap)));
			});
		}



	}
}
