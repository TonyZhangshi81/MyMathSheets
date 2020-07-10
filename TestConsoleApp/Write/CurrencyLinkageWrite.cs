using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 認識價格题型计算式结果显示输出
	/// </summary>
	public class CurrencyLinkageWrite : IConsoleWrite
	{
		/// <summary>
		/// 題型结果显示输出
		/// </summary>
		/// <param name="currencys">價格</param>
		private void ConsoleFormulas(CurrencyLinkageFormula currencys)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "認識價格"));

			int index = 0;
			currencys.LeftCurrencys.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("左邊：{0}   容器編號：{1}", d.ToString("0.00"), currencys.Seats[index++]));
			});

			int seat = 0;
			currencys.Sort.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("右邊{0}：{1}", seat++, currencys.RightCurrencys[d].IntToCurrency().CurrencyToString()));
			});
		}

		/// <summary>
		/// 題型结果显示输出
		/// </summary>
		/// <param name="parameter">參數</param>
		public void ConsoleFormulas(ParameterBase parameter)
		{
			CurrencyLinkageParameter param = (CurrencyLinkageParameter)parameter;

			ConsoleFormulas(param.Currencys);
		}
	}
}