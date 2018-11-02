using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.NumericSorting.Main.Strategy
{
	/// <summary>
	/// 找規律題
	/// </summary>
	[Operation(LayoutSetting.Preview.NumericSorting)]
	public class NumericSorting : OperationBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			NumericSortingParameter p = parameter as NumericSortingParameter;

			List<int> numberList = new List<int>();
			// 出題數量
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				for (var j = 0; j < 10; j++)
				{
					numberList.Add(GetNumberWithSort(numberList, p.MaximumLimit));
				}
				p.Formulas.Add(new NumericSortingFormula()
				{
					// 關係運算符
					Sign = (SignOfCompare)CommonUtil.GetRandomNumber(0, (int)SignOfCompare.Less),
					// 排序數字串(亂序)
					NumberList = numberList.OrderBy(x => Guid.NewGuid()).ToList()
				});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		private int GetNumberWithSort(List<int> list, int maximumLimit)
		{
			int result = CommonUtil.GetRandomNumber(1, maximumLimit);
			if (list.Any(d => d == result))
			{
				result = GetNumberWithSort(list, maximumLimit);
			}
			return result;
		}
	}
}
