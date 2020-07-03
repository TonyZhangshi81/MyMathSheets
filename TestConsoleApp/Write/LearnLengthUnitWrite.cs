using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 認識長度單位题型计算式结果显示输出
	/// </summary>
	public class LearnLengthUnitWrite : IConsoleWrite<List<LearnLengthUnitFormula>>
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<LearnLengthUnitFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "認識長度單位"));

			formulas.ToList().ForEach(d =>
			{
				StringBuilder format = new StringBuilder();
				switch (d.LengthUnitTransType)
				{
					// 米轉換為分米
					case LengthUnitTransformType.M2D:
						format.AppendFormat("({0})米 = ({1})分米  填空項目:{2}", d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, (d.Gap == GapFilling.Left) ? "米" : "分米");
						break;
					// 米轉換為釐米
					case LengthUnitTransformType.M2C:
						format.AppendFormat("({0})米 = ({1})釐米  填空項目:{2}", d.LengthUnitItme.Meter, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "米" : "釐米");
						break;
					// 米轉換為毫米
					case LengthUnitTransformType.M2MM:
						format.AppendFormat("({0})米 = ({1})毫米  填空項目:{2}", d.LengthUnitItme.Meter, d.LengthUnitItme.Millimeter, (d.Gap == GapFilling.Left) ? "米" : "毫米");
						break;

					// 分米轉換為米
					case LengthUnitTransformType.D2M:
						format.AppendFormat("({0})分米 = ({1})米  填空項目:{2}", d.LengthUnitItme.Decimetre, d.LengthUnitItme.Meter, (d.Gap == GapFilling.Left) ? "分米" : "米");
						break;
					// 分米轉換為釐米
					case LengthUnitTransformType.D2C:
						format.AppendFormat("({0})分米 = ({1})釐米  填空項目:{2}", d.LengthUnitItme.Decimetre, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "分米" : "釐米");
						break;
					// 分米轉換為毫米
					case LengthUnitTransformType.D2MM:
						format.AppendFormat("({0})分米 = ({1})毫米  填空項目:{2}", d.LengthUnitItme.Decimetre, d.LengthUnitItme.Millimeter, (d.Gap == GapFilling.Left) ? "分米" : "毫米");
						break;
					// 分米到米分米
					case LengthUnitTransformType.D2MExt:
						format.AppendFormat("({0})分米 = ({1})米({2})分米  填空項目:{3}", d.LengthUnitItme.Decimetre, d.LengthUnitItme.Meter, d.RemainderDecimetre, (d.Gap == GapFilling.Left) ? "分米" : "米,分米");
						break;
					// 分米到米釐米
					case LengthUnitTransformType.D2MC:
						format.AppendFormat("({0})分米 = ({1})米({2})釐米  填空項目:{3}", d.LengthUnitItme.Decimetre, d.LengthUnitItme.Meter, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "分米" : "米,釐米");
						break;

					// 釐米到米
					case LengthUnitTransformType.C2M:
						format.AppendFormat("({0})釐米 = ({1})米  填空項目:{2}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Meter, (d.Gap == GapFilling.Left) ? "釐米" : "米");
						break;
					// 釐米到分米
					case LengthUnitTransformType.C2D:
						format.AppendFormat("({0})釐米 = ({1})分米  填空項目:{2}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Decimetre, (d.Gap == GapFilling.Left) ? "釐米" : "分米");
						break;
					// 釐米到毫米
					case LengthUnitTransformType.C2MM:
						format.AppendFormat("({0})釐米 = ({1})毫米  填空項目:{2}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Millimeter, (d.Gap == GapFilling.Left) ? "釐米" : "毫米");
						break;
					// 釐米到米分米
					case LengthUnitTransformType.C2MD:
						format.AppendFormat("({0})釐米 = ({1})米({2})分米  填空項目:{3}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, (d.Gap == GapFilling.Left) ? "釐米" : "米,分米");
						break;
					// 釐米到分米毫米
					case LengthUnitTransformType.C2DMM:
						format.AppendFormat("({0})釐米 = ({1})分米({2})毫米  填空項目:{3}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Decimetre, d.LengthUnitItme.Millimeter, (d.Gap == GapFilling.Left) ? "釐米" : "分米,毫米");
						break;
					// 釐米到米分米釐米
					case LengthUnitTransformType.C2MDExt:
						format.AppendFormat("({0})釐米 = ({1})米({2})分米({3})釐米  填空項目:{4}", d.LengthUnitItme.Centimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, d.RemainderCentimeter, (d.Gap == GapFilling.Left) ? "釐米" : "米,分米,釐米");
						break;

					default:
						break;

					// 毫米到米
					case LengthUnitTransformType.MM2M:
						format.AppendFormat("({0})毫米 = ({1})米  填空項目:{2}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Meter, (d.Gap == GapFilling.Left) ? "毫米" : "米");
						break;
					// 毫米到分米
					case LengthUnitTransformType.MM2D:
						format.AppendFormat("({0})毫米 = ({1})分米  填空項目:{2}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Decimetre, (d.Gap == GapFilling.Left) ? "毫米" : "分米");
						break;
					// 毫米到釐米
					case LengthUnitTransformType.MM2C:
						format.AppendFormat("({0})毫米 = ({1})釐米  填空項目:{2}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "毫米" : "釐米");
						break;
					// 毫米到米分米
					case LengthUnitTransformType.MM2MD:
						format.AppendFormat("({0})毫米 = ({1})米({2})分米  填空項目:{3}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, (d.Gap == GapFilling.Left) ? "毫米" : "米,分米");
						break;
					// 毫米到米分米釐米
					case LengthUnitTransformType.MM2MDC:
						format.AppendFormat("({0})毫米 = ({1})米({2})分米({3})釐米  填空項目:{4}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "毫米" : "米,分米,釐米");
						break;
					// 毫米到米分米釐米
					case LengthUnitTransformType.MM2DC:
						format.AppendFormat("({0})毫米 = ({1})分米({2})釐米  填空項目:{3}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Decimetre, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "毫米" : "分米,釐米");
						break;
					// 毫米到米釐米
					case LengthUnitTransformType.MM2MC:
						format.AppendFormat("({0})毫米 = ({1})米({2})釐米  填空項目:{3}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Centimeter, (d.Gap == GapFilling.Left) ? "毫米" : "米,釐米");
						break;
					// 毫米到米分米釐米毫米
					case LengthUnitTransformType.MM2MDCExt:
						format.AppendFormat("({0})毫米 = ({1})米({2})分米({3})釐米({4})毫米  填空項目:{5}", d.LengthUnitItme.Millimeter, d.LengthUnitItme.Meter, d.LengthUnitItme.Decimetre, d.LengthUnitItme.Centimeter, d.RemainderMillimeter, (d.Gap == GapFilling.Left) ? "毫米" : "米,分米,釐米,毫米");
						break;
				}

				Console.WriteLine(format);
			});
		}
	}
}