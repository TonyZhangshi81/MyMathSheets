﻿using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
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
	/// 認識貨幣题型计算式结果显示输出
	/// </summary>
	[TopicWrite("LearnCurrency")]
	public class LearnCurrencyWrite : TopicWriteBase<LearnCurrencyParameter>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<LearnCurrencyFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "認識貨幣"));

			formulas.ToList().ForEach(d =>
			{
				StringBuilder format = new StringBuilder();
				switch (d.CurrencyTransType)
				{
					// 元轉角
					case CurrencyTransformType.Y2J:
						format.AppendFormat("元({0}) = 角({1})  填空項目:{2}", d.CurrencyUnit.Yuan, d.CurrencyUnit.Jiao, (d.Gap == GapFilling.Left) ? "元" : "角");
						break;
					// 元轉分
					case CurrencyTransformType.Y2F:
						format.AppendFormat("元({0}) = 分({1})  填空項目:{2}", d.CurrencyUnit.Yuan, d.CurrencyUnit.Fen, (d.Gap == GapFilling.Left) ? "元" : "分");
						break;
					// 角轉元
					case CurrencyTransformType.J2Y:
						format.AppendFormat("角({0}) = 元({1})  填空項目:{2}", d.CurrencyUnit.Jiao, d.CurrencyUnit.Yuan, (d.Gap == GapFilling.Left) ? "角" : "元");
						break;
					// 角轉分
					case CurrencyTransformType.J2F:
						format.AppendFormat("角({0}) = 分({1})  填空項目:{2}", d.CurrencyUnit.Jiao, d.CurrencyUnit.Fen, (d.Gap == GapFilling.Left) ? "角" : "分");
						break;
					// 角轉元分
					case CurrencyTransformType.J2YF:
						format.AppendFormat("角({0}) = 元({1})分({2})  填空項目:{3}", d.CurrencyUnit.Jiao, d.CurrencyUnit.Yuan, d.CurrencyUnit.Fen, (d.Gap == GapFilling.Left) ? "角" : "元、分");
						break;
					// 元轉分
					case CurrencyTransformType.F2Y:
						format.AppendFormat("分({0}) = 元({1})  填空項目:{2}", d.CurrencyUnit.Fen, d.CurrencyUnit.Yuan, (d.Gap == GapFilling.Left) ? "分" : "元");
						break;
					// 元轉角
					case CurrencyTransformType.F2J:
						format.AppendFormat("分({0}) = 角({1})  填空項目:{2}", d.CurrencyUnit.Fen, d.CurrencyUnit.Jiao, (d.Gap == GapFilling.Left) ? "分" : "角");
						break;
					// 分轉元角
					case CurrencyTransformType.F2YJ:
						format.AppendFormat("分({0}) = 元({1})角({2})  填空項目:{3}", d.CurrencyUnit.Fen, d.CurrencyUnit.Yuan, d.CurrencyUnit.Jiao, (d.Gap == GapFilling.Left) ? "分" : "元、角");
						break;
					// 角轉元（有剩餘）
					case CurrencyTransformType.J2YExt:
						format.AppendFormat("角({0}) = 元({1})角({2})  填空項目:{3}", d.CurrencyUnit.Jiao, d.CurrencyUnit.Yuan, d.RemainderJiao.Value,
											(d.Gap == GapFilling.Left) ? "角" : "元角");
						break;
					// 分轉元角（有剩餘）
					case CurrencyTransformType.F2YJExt:
						format.AppendFormat("分({0}) = 元({1})角({2})分({3})  填空項目:{4}", d.CurrencyUnit.Fen, d.CurrencyUnit.Yuan, d.CurrencyUnit.Jiao, d.RemainderFen.Value,
											(d.Gap == GapFilling.Left) ? "分" : "元角分");
						break;
				}

				Console.WriteLine(format);
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public override void ConsoleFormulas(LearnCurrencyParameter parameter)
		{
			ConsoleFormulas(parameter.Formulas);
		}
	}
}