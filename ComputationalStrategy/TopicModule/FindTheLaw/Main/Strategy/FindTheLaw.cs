using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Strategy
{
	/// <summary>
	/// 找規律題
	/// </summary>
	[Operation(LayoutSetting.Preview.FindTheLaw)]
	public class FindTheLaw : OperationBase
	{
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			FindTheLawParameter p = parameter as FindTheLawParameter;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 隨機抽取找規律題型的算法種類
				FindTheLawLevel lawLevel = CommonUtil.GetRandomNumber(FindTheLawLevel.Crescent, FindTheLawLevel.Superposition);
				switch (lawLevel)
				{
					// 定量遞增
					case FindTheLawLevel.Crescent:
						p.Formulas.Add(Crescent(p));
						break;

					// 定量遞增
					case FindTheLawLevel.Diminishingly:
						p.Formulas.Add(Diminishingly(p));
						break;

					// 疊加遞增
					case FindTheLawLevel.Superposition:
						p.Formulas.Add(Superposition(p));
						break;
				}
			}
		}

		/// <summary>
		/// 疊加遞歸次數
		/// </summary>
		private int _superpositionThreshold = 0;
		/// <summary>
		/// 疊加遞增算法
		/// </summary>
		/// <param name="p">題型參數</param>
		private FindTheLawFormula Superposition(FindTheLawParameter p)
		{
			// 第一個開始的數值（如果遞歸次數大於3此時，使用默認值100作為開始值）
			var start = _superpositionThreshold >= 3 ? 5 : CommonUtil.GetRandomNumber(0, p.MaximumLimit);
			// 遞減量
			var lessenValue = start;
			// 如果遞減后結果小於0時，當前算法遞歸
			if ((start + lessenValue * 6) > 100)
			{
				_superpositionThreshold++;
				// 重新推算并返回結果
				return Superposition(p);
			}

			// 構成
			List<int> numberList = new List<int>() { start };
			List<int> randomIndexList = new List<int>() { 0 };
			for (var index = 1; index < 7; index++)
			{
				start += lessenValue;
				numberList.Add(start);
				randomIndexList.Add(index);
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = RandomIndexListOrder(randomIndexList)
			};
			formula.RandomIndexList.Sort();

			// 遞歸次數初期化
			_superpositionThreshold = 0;
			// 結果返回
			return formula;
		}


		/// <summary>
		/// 遞減遞歸次數
		/// </summary>
		private int _diminishinglyThreshold = 0;
		/// <summary>
		/// 定量遞減算法
		/// </summary>
		/// <param name="p">題型參數</param>
		private FindTheLawFormula Diminishingly(FindTheLawParameter p)
		{
			// 第一個開始的數值（如果遞歸次數大於3此時，使用默認值100作為開始值）
			var start = _diminishinglyThreshold >= 3 ? 100 : CommonUtil.GetRandomNumber(0, p.MaximumLimit);
			// 遞減量
			var lessenValue = CommonUtil.GetRandomNumber(3, 8);
			// 如果遞減后結果小於0時，當前算法遞歸
			if ((start - lessenValue * 6) < 0)
			{
				_diminishinglyThreshold++;
				// 重新推算并返回結果
				return Diminishingly(p);
			}

			// 構成
			List<int> numberList = new List<int>() { start };
			List<int> randomIndexList = new List<int>() { 0 };
			for (var index = 1; index < 7; index++)
			{
				start -= lessenValue;
				numberList.Add(start);
				randomIndexList.Add(index);
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = RandomIndexListOrder(randomIndexList)
			};
			formula.RandomIndexList.Sort();

			// 遞歸次數初期化
			_diminishinglyThreshold = 0;
			// 結果返回
			return formula;
		}

		/// <summary>
		/// 新處理隨機填空項目
		/// </summary>
		/// <param name="list">所有項目的序號列表</param>
		/// <returns></returns>
		private List<int> RandomIndexListOrder(List<int> list)
		{
			// 隨機填空項目
			list = list.OrderBy(x => Guid.NewGuid()).ToList();
			// 取得前三個序號
			List<int> randomIndexList = new List<int>() { list[0], list[1], list[2] };
			// 并排序
			randomIndexList.Sort();

			// index: 0,1,2,3,4,5,6
			if (randomIndexList[0] >= 3 || randomIndexList[2] < 4 || randomIndexList[1] - randomIndexList[0] > 3 || randomIndexList[2] - randomIndexList[1] > 3)
			{
				randomIndexList.Sort();
				// 隨機填空項目的順次原則是保證至少有3個已知數是連續的(便於題型解答)
				return randomIndexList;
			}
			// 如果為滿足上述條件則遞歸處理(重新處理隨機填空項目)
			return RandomIndexListOrder(list);
		}

		/// <summary>
		/// 遞增遞歸次數
		/// </summary>
		private int _crescentThreshold = 0;
		/// <summary>
		/// 定量遞增算法
		/// </summary>
		/// <param name="p">題型參數</param>
		private FindTheLawFormula Crescent(FindTheLawParameter p)
		{
			// 第一個開始的數值（如果遞歸次數大於3此時，使用默認值1作為開始值）
			var start = _crescentThreshold >= 3 ? 1 : CommonUtil.GetRandomNumber(0, p.MaximumLimit);
			// 遞增量
			var addValue = CommonUtil.GetRandomNumber(3, 8);
			// 如果遞增后結果大於100時，當前算法遞歸
			if ((start + addValue * 6) > 100)
			{
				_crescentThreshold++;
				// 重新推算并返回結果
				return Crescent(p);
			}

			// 構成
			List<int> numberList = new List<int>() { start };
			List<int> randomIndexList = new List<int>() { 0 };
			for (var index = 1; index < 7; index++)
			{
				start += addValue;
				numberList.Add(start);
				randomIndexList.Add(index);
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = RandomIndexListOrder(randomIndexList)
			};
			formula.RandomIndexList.Sort();

			// 遞歸次數初期化
			_crescentThreshold = 0;
			// 結果返回
			return formula;
		}

	}
}
