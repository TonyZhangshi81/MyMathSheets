using MyMathSheets.CommonLib.Main.ArithmeticStrategy;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Operation;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.Operation
{
	/// <summary>
	/// 水果連連看題型構築
	/// </summary>
	public class FruitsLinkage : SetThemeBase<FruitsLinkageFormula>
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 水果連連看題型構築對象初期化
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public FruitsLinkage(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, int maximumLimit, int numberOfQuestions) :
			base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		public override void MarkFormulaList()
		{
			if (_fourOperationsType == FourOperationsType.Default)
			{
				return;
			}

			ICalculate strategy = null;

			// 水果連連看對象實例
			FruitsLinkageFormula fruitsLinkageFormula = new FruitsLinkageFormula();

			// 左邊水果算式實例
			IList<Formula> fruitsFormulas = new List<Formula>();
			// 右邊容器算式實例
			IList<Formula> containersFormulas = new List<Formula>();
			// 容器座位號
			IList<int> seats = new List<int>();

			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 標準題型（指定單個運算符）
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = CalculateManager.CreateCalculateInstance(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 計算式作成
					Formula fruit = strategy.CreateFormula(_maximumLimit, QuestionType.Standard, 0);
					fruitsFormulas.Add(fruit);
					// 計算式作成（依據水果算式的答案推算容器算式）
					Formula container = strategy.CreateFormulaWithAnswer(_maximumLimit, fruit.Answer);
					containersFormulas.Add(container);
					// 容器座位號
					seats.Add(i);

					if (CheckIsNeedInverseMethod(fruitsFormulas, containersFormulas, fruit.Answer))
					{
						defeated++;
						// 移除當前推算
						fruitsFormulas.Remove(fruitsFormulas.Last());
						containersFormulas.Remove(containersFormulas.Last());
						seats.Remove(seats.Last());

						// 如果大於兩次則認為此題無法作成繼續下一題
						if (defeated == INVERSE_NUMBER)
						{
							// 當前反推判定次數復原
							defeated = 0;
							continue;
						}
						i--;
						continue;
					}

					// 當前反推判定次數復原
					defeated = 0;
				}
			}
			else
			{
				RandomNumberComposition random = null;
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					random = new RandomNumberComposition(0, _signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					SignOfOperation sign = _signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager.CreateCalculateInstance(sign);
					// 計算式作成
					Formula fruit = strategy.CreateFormula(_maximumLimit, QuestionType.Standard, 0, GapFilling.Default);
					// 水果算式列表添加
					fruitsFormulas.Add(fruit);

					random = new RandomNumberComposition(0, _signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					sign = _signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager.CreateCalculateInstance(sign);
					// 計算式作成（依據水果算式的答案推算容器算式）
					Formula container = strategy.CreateFormulaWithAnswer(_maximumLimit, fruit.Answer);
					// 容器算式列表添加
					containersFormulas.Add(container);

					// 容器座位號
					seats.Add(i);

					if (CheckIsNeedInverseMethod(fruitsFormulas, containersFormulas, fruit.Answer))
					{
						defeated++;
						// 移除當前推算
						fruitsFormulas.Remove(fruitsFormulas.Last());
						containersFormulas.Remove(containersFormulas.Last());
						seats.Remove(seats.Last());

						// 如果大於兩次則認為此題無法作成繼續下一題
						if (defeated == INVERSE_NUMBER)
						{
							// 當前反推判定次數復原
							defeated = 0;
							continue;
						}
						i--;
						continue;
					}

					// 當前反推判定次數復原
					defeated = 0;
				}
			}

			// 左邊水果算式
			fruitsLinkageFormula.FruitsFormulas = fruitsFormulas;
			// 右側容器算式
			fruitsLinkageFormula.ContainersFormulas = containersFormulas;
			// 座位號隨機排序
			fruitsLinkageFormula.Sort = seats.OrderBy(x => Guid.NewGuid()).ToList();
			// 容器的擺放位置
			fruitsLinkageFormula.Seats = GetNewSeats(fruitsLinkageFormula.Sort);

			// 結果設定
			_formulas = fruitsLinkageFormula;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="seats"></param>
		/// <returns></returns>
		private IList<int> GetNewSeats(IList<int> seats)
		{
			IList<int> newSeats = new List<int>();
			for(int number = 0; number < seats.Count; number++)
			{
				newSeats.Add(seats.IndexOf(number));
			}
			return newSeats;
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式結果存在一致
		/// 情況2：無法根據當前計算式的結果推算下一個計算式（一般出現在無法被整除或者超過九九乘法口訣上限值【81】時）
		/// 情況3：水果計算式與容器計算式完全一致
		/// </remarks>
		/// <param name="fruitsFormulas">左側水果計算式集合</param>
		/// <param name="answer">計算結果值</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<Formula> fruitsFormulas, IList<Formula> containersFormulas, int answer)
		{
			// 情況1
			if (fruitsFormulas.ToList().Count(d => d.Answer == answer) >= 2)
			{
				return true;
			}
			// 情況2
			if (fruitsFormulas.Last().IsNoSolution || containersFormulas.Last().IsNoSolution)
			{
				return true;
			}
			// 情況3
			if (fruitsFormulas.Last().LeftParameter == containersFormulas.Last().LeftParameter
				&& fruitsFormulas.Last().RightParameter == containersFormulas.Last().RightParameter
				&& fruitsFormulas.Last().Sign == containersFormulas.Last().Sign)
			{
				return true;
			}
			return false;
		}
	}
}
