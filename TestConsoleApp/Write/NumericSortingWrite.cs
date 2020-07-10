using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.ComputationalStrategy.NumericSorting.Item;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
using MyMathSheets.TestConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 數字排序題型計算式結果顯示輸出
	/// </summary>
	public class NumericSortingWrite : IConsoleWrite
	{
		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(IList<NumericSortingFormula> formulas)
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004T, "數字排序"));

			formulas.ToList().ForEach(d =>
			{
				StringBuilder str = new StringBuilder();

				// 關係運算符輸出
				Console.WriteLine("關係運算符:{0}", d.Sign.ToSignOfCompareString());
				// 無序字符串輸出
				d.NumberList.ForEach(n =>
				{
					str.AppendFormat("{0},", n);
				});
				str.Length -= 1;
				Console.WriteLine("題目:{0}", str);

				str.Length = 0;
				// 有序字符串輸出
				d.AnswerList.ForEach(n =>
				{
					str.AppendFormat("{0}{1}", n, d.Sign.ToSignOfCompareString());
				});
				str.Length -= 1;
				Console.WriteLine("答案:{0}", str);
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		public void ConsoleFormulas(ParameterBase parameter)
		{
			NumericSortingParameter param = (NumericSortingParameter)parameter;

			ConsoleFormulas(param.Formulas);
		}
	}
}