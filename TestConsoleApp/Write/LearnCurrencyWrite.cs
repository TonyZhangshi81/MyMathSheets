using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 認識貨幣题型计算式结果显示输出
	/// </summary>
	public class LearnCurrencyWrite : IConsoleWrite<List<LearnCurrencyFormula>>
	{
		private static Log log = Log.LogReady(typeof(LearnCurrencyWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<LearnCurrencyFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "認識貨幣"));

			formulas.ToList().ForEach(d =>
			{

			});
		}
	}
}
