using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Strategy
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
				FindTheLawLevel lawLevel = (FindTheLawLevel)CommonUtil.GetRandomNumber(0, (int)FindTheLawLevel.Superposition);
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
			// 隨機填空項目
			randomIndexList = randomIndexList.OrderBy(x => Guid.NewGuid()).ToList();

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = new List<int>()
				{
					randomIndexList[0], randomIndexList[1], randomIndexList[2]
				}
			};

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
			// 隨機填空項目
			randomIndexList = randomIndexList.OrderBy(x => Guid.NewGuid()).ToList();

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = new List<int>()
				{
					randomIndexList[0], randomIndexList[1], randomIndexList[2]
				}
			};

			// 遞歸次數初期化
			_diminishinglyThreshold = 0;
			// 結果返回
			return formula;
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
			// 隨機填空項目
			randomIndexList = randomIndexList.OrderBy(x => Guid.NewGuid()).ToList();

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 隨機項目編號(填空項目)
				RandomIndexList = new List<int>()
				{
					randomIndexList[0], randomIndexList[1], randomIndexList[2]
				}
			};
			// 遞歸次數初期化
			_crescentThreshold = 0;
			// 結果返回
			return formula;
		}

	}
}
