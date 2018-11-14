using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
				StringBuilder format = new StringBuilder();
				switch (d.CurrencyTransformType)
				{
					// 元轉角
					case CurrencyTransform.Y2J:
						format.AppendFormat("元({0}) = 角({1})  填空項目:{2}", d.YuanUnit, d.JiaoUnit, (d.Gap == GapFilling.Left) ? "元" : "角");
						break;
					// 元轉分
					case CurrencyTransform.Y2F:
						format.AppendFormat("元({0}) = 分({1})  填空項目:{2}", d.YuanUnit, d.FenUnit, (d.Gap == GapFilling.Left) ? "元" : "分");
						break;
					// 角轉元
					case CurrencyTransform.J2Y:
						format.AppendFormat("角({0}) = 元({1})  填空項目:{2}", d.JiaoUnit, d.YuanUnit, (d.Gap == GapFilling.Left) ? "角" : "元");
						break;
					// 角轉分
					case CurrencyTransform.J2F:
						format.AppendFormat("角({0}) = 分({1})  填空項目:{2}", d.JiaoUnit, d.YuanUnit, (d.Gap == GapFilling.Left) ? "角" : "分");
						break;
				}

				Console.WriteLine(format);
			});
		}
	}
}
