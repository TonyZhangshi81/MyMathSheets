using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.TestConsoleApp.Write
{
	/// <summary>
	/// 數字排序題型計算式結果顯示輸出
	/// </summary>
	public class NumericSortingWrite : IConsoleWrite<List<NumericSortingFormula>>
	{
		private static Log log = Log.LogReady(typeof(NumericSortingWrite));

		/// <summary>
		/// 计算式结果显示输出
		/// </summary>
		/// <param name="formulas">计算式</param>
		public void ConsoleFormulas(List<NumericSortingFormula> formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004T, "數字排序"));

			formulas.ForEach(d => {
				Console.WriteLine("關係運算符:{0}", d.Sign.ToSignOfCompareString());
				StringBuilder str = new StringBuilder();
				d.NumberList.ForEach(n => {
					
					str.AppendFormat("{0},", n);
				});
				str.Length -= 1;
				Console.WriteLine("題目:{0}", str);

				
				if (d.Sign == SignOfCompare.Greater)
				{
					d.NumberList.Sort((x, y) => -x.CompareTo(y));
				}
				else
				{
					d.NumberList.Sort();
				}

				str.Length = 0;
				d.NumberList.ForEach(n => {

					str.AppendFormat("{0}{1}", n, d.Sign.ToSignOfCompareEnString());
				});
				str.Length -= 1;
				Console.WriteLine("題目:{0}", str);
			});

			
		}
	}
}
