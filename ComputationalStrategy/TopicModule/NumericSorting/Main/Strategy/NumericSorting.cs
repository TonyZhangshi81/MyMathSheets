using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.NumericSorting.Item;
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

			// 出題數量
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				List<int> numberList = new List<int>();
				List<int> answerList = new List<int>();
				// 參與數字排序的對象個數
				for (var j = 0; j < p.Numbers; j++)
				{
					numberList.Add(GetNumberWithSort(numberList, p.MaximumLimit));
				}

				// 關係運算符(隨機獲取)
				SignOfCompare sign = (SignOfCompare)CommonUtil.GetRandomNumber(0, (int)SignOfCompare.Less);
				// 如果是大於符號
				if (sign == SignOfCompare.Greater)
				{
					// 以降序方式排列數組并作為答案保存
					numberList.Sort((x, y) => -x.CompareTo(y));
				}
				else
				{
					// 以升序方式排列數組并作為答案保存
					numberList.Sort();
				}
				answerList = numberList;


				p.Formulas.Add(new NumericSortingFormula()
				{
					// 關係運算符
					Sign = sign,
					// 排序數字串(亂序)
					NumberList = numberList.OrderBy(x => Guid.NewGuid()).ToList(),
					// 答案數字串(已排序)
					AnswerList = answerList
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
