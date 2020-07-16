using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 四則運算题型计算式结果显示输出
	/// </summary>
	public class ArithmeticOperationsWrite : IConsoleWrite
	{
		/// <summary>
		/// 計算結果顯示輸出
		/// </summary>
		/// <param name="parameter">參數</param>
		public void ConsoleFormulas(TopicParameterBase parameter)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "四則運算"));

			ArithmeticOperationsParameter param = (ArithmeticOperationsParameter)parameter;

			param.Formulas.ToList().ForEach(d =>
			{
				if (d.AnswerIsRight)
				{
					Console.WriteLine(string.Format("{0} {1} {2} = {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap)));
				}
				else
				{
					Console.WriteLine(string.Format("{0} = {1} {2} {3}",
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Answer, d.Arithmetic.Answer, d.Arithmetic.Gap),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Left, d.Arithmetic.LeftParameter, d.Arithmetic.Gap),
						d.Arithmetic.Sign.ToOperationString(),
						CommonUtil.GetValue(CommonLib.Util.GapFilling.Right, d.Arithmetic.RightParameter, d.Arithmetic.Gap)));
				}
			});
		}
	}
}