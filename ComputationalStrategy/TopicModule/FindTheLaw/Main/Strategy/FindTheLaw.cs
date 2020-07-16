using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Strategy
{
	/// <summary>
	/// 找規律題
	/// </summary>
	[Operation("FindTheLaw")]
	public class FindTheLaw : TopicBase
	{
		/// <summary>
		/// 找規律題型的實現方法集合
		/// </summary>
		private readonly Dictionary<FindTheLawLevel, Func<FindTheLawFormula>> _findTheLawActions = new Dictionary<FindTheLawLevel, Func<FindTheLawFormula>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		public FindTheLaw()
		{
			// 定量遞增
			_findTheLawActions[FindTheLawLevel.Crescent] = Crescent;
			// 定量遞減
			_findTheLawActions[FindTheLawLevel.Diminishingly] = Diminishingly;
			// 變量遞增
			_findTheLawActions[FindTheLawLevel.Variable] = Variable;
			// 變量遞減
			_findTheLawActions[FindTheLawLevel.Decrement] = Decrement;
			// 疊加遞增
			_findTheLawActions[FindTheLawLevel.Superposition] = Superposition;
			// 定量遞增擴展（2個定值逐個遞增）
			_findTheLawActions[FindTheLawLevel.CrescentExt] = CrescentExt;
			// 定量遞減擴展（2個定值逐個遞減）
			_findTheLawActions[FindTheLawLevel.DiminishinglyExt] = DiminishinglyExt;
			// 數字排隊（從4個隨機數的排列中找規律）
			_findTheLawActions[FindTheLawLevel.QueueUpBase] = QueueUpBase;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			FindTheLawParameter p = parameter as FindTheLawParameter;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 隨機抽取找規律題型的算法種類
				FindTheLawLevel lawLevel = (FindTheLawLevel)CommonUtil.GetRandomNumber(p.Types.ToList());

				if (_findTheLawActions.TryGetValue(lawLevel, out Func<FindTheLawFormula> findTheLawAction))
				{
					p.Formulas.Add(findTheLawAction());
				}
				else
				{
					throw new ArgumentException(MessageUtil.GetMessage(() => MsgResources.E0001L, lawLevel.ToString()));
				}
			}
		}

		/// <summary>
		/// 數字排隊（從4個隨機數的排列中找規律）
		/// </summary>
		/// <remarks>eg: 1234,2341,3412,4123,1234</remarks>
		private FindTheLawFormula QueueUpBase()
		{
			List<int> numbers = new List<int>();
			StringBuilder number = new StringBuilder();
			List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int count = 0;
			// 數字隊列初始化
			while (true)
			{
				if (count == 4)
				{
					// 初始化創建5位以便前4位數字的輪轉
					numbers.Add(numbers[0]);
					break;
				}
				numbers.Add(CommonUtil.GetRandomNumber(list));
				list.Remove(numbers.Last());
				count++;
			}

			// 構成
			List<int> numberList = new List<int>
			{
				Convert.ToInt32(string.Format("{0}{1}{2}{3}", numbers[0], numbers[1], numbers[2], numbers[3]))
			};

			count = 0;
			// 數字隊列集合作成
			while (true)
			{
				if (count == 4)
				{
					break;
				}

				number.Length = 0;
				// 數字隊列輪轉(後一位數逐個逐個往前移動一位)
				for (int index = 1; index <= numbers.Count; index++)
				{
					if (index == numbers.Count)
					{
						// 末尾放起始位上的數字
						numbers[numbers.Count - 1] = numbers[0];
					}
					else
					{
						// 後一位數往前移動
						numbers[index - 1] = numbers[index];
						number.Append(numbers[index - 1]);
					}
				}

				numberList.Add(Convert.ToInt32(number.ToString()));
				count++;
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 填空項目編號
				RandomIndexList = new List<int>() { 2, 3 }
			};

			// 結果返回
			return formula;
		}

		/// <summary>
		/// 定量遞減擴展（2個定值逐個遞減）
		/// </summary>
		/// <remarks>eg: 18,17,13,12,8,7,3,2</remarks>
		private FindTheLawFormula DiminishinglyExt()
		{
			// 使用定量遞增擴展算法
			var formula = CrescentExt();
			// 在定量遞增擴展算法的結果序列上進行降序設定（獲得遞減序列）
			formula.NumberList.Sort((x, y) => -x.CompareTo(y));

			return formula;
		}

		/// <summary>
		/// 定量遞增擴展（2個定值逐個遞增）
		/// </summary>
		/// <remarks>eg: 2,3,7,8,12,13,17,18</remarks>
		private FindTheLawFormula CrescentExt()
		{
			// 第一個開始的數值
			var start = CommonUtil.GetRandomNumber(1, 5);
			// 遞增量
			var addValues = new List<int>() { CommonUtil.GetRandomNumber(1, 3), CommonUtil.GetRandomNumber(4, 6) };

			// 構成
			List<int> numberList = new List<int>() { start };
			for (var index = 1; index < 8; index++)
			{
				start += addValues[(index + 1) % 2];
				numberList.Add(start);
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 填空項目編號
				RandomIndexList = new List<int>() { 5, 6, 7 }
			};

			// 結果返回
			return formula;
		}

		/// <summary>
		/// 疊加遞增
		/// </summary>
		/// <remarks>eg: 1,2,3,5,8,13,21</remarks>
		private FindTheLawFormula Superposition()
		{
			// 第一個開始的數值
			var one = CommonUtil.GetRandomNumber(1, 3);
			// 第一個開始的數值
			var two = CommonUtil.GetRandomNumber(1, 3);

			// 構成
			List<int> numberList = new List<int>() { one, two };
			for (var index = 2; index < 8; index++)
			{
				numberList.Add(numberList[index - 2] + numberList[index - 1]);
			}

			FindTheLawFormula formula = new FindTheLawFormula()
			{
				// 自然數列表
				NumberList = numberList,
				// 填空項目編號
				RandomIndexList = new List<int>() { 5, 6, 7 }
			};

			// 結果返回
			return formula;
		}

		/// <summary>
		/// 變量遞減算法
		/// </summary>
		/// <remarks>eg: 20,17,14,11,8,5,2</remarks>
		private FindTheLawFormula Decrement()
		{
			// 使用變量遞增算法
			var formula = Variable();
			// 在變量遞增算法的結果序列上進行降序設定（獲得遞減序列）
			formula.NumberList.Sort((x, y) => -x.CompareTo(y));

			return formula;
		}

		/// <summary>
		/// 變量遞增
		/// </summary>
		/// <remarks>eg: 2,3,5,8,12,17</remarks>
		private FindTheLawFormula Variable()
		{
			// 第一個開始的數值
			var start = CommonUtil.GetRandomNumber(1, 5);

			// 構成
			List<int> numberList = new List<int>() { start };
			List<int> randomIndexList = new List<int>() { 0 };
			for (var index = 1; index < 7; index++)
			{
				start += index;
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

			// 結果返回
			return formula;
		}

		/// <summary>
		/// 定量遞減算法
		/// </summary>
		/// <remarks>eg: 20,17,14,11,8,5,2</remarks>
		private FindTheLawFormula Diminishingly()
		{
			// 使用遞增算法
			var formula = Crescent();
			// 在遞增算法的結果序列上進行降序設定（獲得遞減序列）
			formula.NumberList.Sort((x, y) => -x.CompareTo(y));

			return formula;
		}

		/// <summary>
		/// 隨機三個序號作為填空項目
		/// </summary>
		/// <param name="list">所有項目的序號列表</param>
		/// <returns>填空項目序列</returns>
		private List<int> RandomIndexListOrder(List<int> list)
		{
			// 隨機填空項目
			list = list.OrderBy(x => Guid.NewGuid()).ToList();
			// 取得前三個序號作為填空項目
			List<int> randomIndexList = new List<int>() { list[0], list[1], list[2] };
			// 排序
			randomIndexList.Sort();

			// index: 0,1,2,3,4,5,6
			if (randomIndexList[0] >= 3 || randomIndexList[2] < 4 || randomIndexList[1] - randomIndexList[0] > 3 || randomIndexList[2] - randomIndexList[1] > 3)
			{
				// 隨機填空項目的順次原則是保證至少有3個已知數是連續的(便於題型解答)
				return randomIndexList;
			}
			// 避免填空項目是以間隔排列的
			else if ((randomIndexList[0] == 1 && randomIndexList[1] == 3 && randomIndexList[2] == 5) ||
				(randomIndexList[0] == 0 && randomIndexList[1] == 2 && randomIndexList[2] == 4) ||
				(randomIndexList[0] == 2 && randomIndexList[1] == 4 && randomIndexList[2] == 6))
			{
				// 如果為滿足上述條件則遞歸處理(重新處理隨機填空項目)
				return RandomIndexListOrder(list);
			}
			return randomIndexList;
		}

		/// <summary>
		/// 定量遞增算法
		/// </summary>
		/// <remarks>eg: 2,5,8,11,14,17,20</remarks>
		private FindTheLawFormula Crescent()
		{
			// 第一個開始的數值
			var start = CommonUtil.GetRandomNumber(1, 5);
			// 遞增量
			var addValue = CommonUtil.GetRandomNumber(5, 9);

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

			// 結果返回
			return formula;
		}
	}
}