using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Item;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Strategy
{
	/// <summary>
	/// 水果連連看題型構築
	/// </summary>
	[Topic("FruitsLinkage")]
	public class FruitsLinkage : TopicBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 5;

		// 左邊水果算式實例
		private readonly IList<Formula> _fruitsFormulas;

		// 右邊容器算式實例
		private readonly IList<Formula> _containersFormulas;

		// 各計算式編號的集合
		private readonly IList<int> _seats;

		/// <summary>
		/// 構造函數
		/// </summary>
		public FruitsLinkage()
		{
			_fruitsFormulas = new List<Formula>();
			// 右邊容器算式實例
			_containersFormulas = new List<Formula>();
			// 各計算式編號的集合
			_seats = new List<int>();
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>作成是否成功</returns>
		private void MarkFormulaList(FruitsLinkageParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// (水果個數的個數最多10個)
			p.NumberOfQuestions = (p.NumberOfQuestions > 10) ? 10 : p.NumberOfQuestions;

			int seatNumber = 0;
			// 按照指定數量作成相應的數學計算式
			for (int i = 0; i < p.NumberOfQuestions; i++, seatNumber++)
			{
				// 計算式作成（指定單個運算符實例）
				Formula fruit = MakeLeftFormulas(_fruitsFormulas, p.MaximumLimit, signFunc);
				// 計算式作成（依據水果算式的答案推算容器算式）
				MakeRightFormulas(_containersFormulas, p.MaximumLimit, fruit.Answer, signFunc);
				// 各計算式編號的集合
				_seats.Add(seatNumber);

				if (CheckIsNeedInverseMethod(_fruitsFormulas, _containersFormulas, fruit.Answer))
				{
					defeated++;
					// 移除當前推算
					_fruitsFormulas.Remove(_fruitsFormulas.Last());
					_containersFormulas.Remove(_containersFormulas.Last());
					_seats.Remove(_seats.Last());

					// 如果大於五次則認為此題無法作成繼續下一題
					if (defeated == INVERSE_NUMBER)
					{
						// 當前反推判定次數復原
						defeated = 0;
						seatNumber--;
						continue;
					}
					i--;
					seatNumber--;
					continue;
				}
				// 當前反推判定次數復原
				defeated = 0;
			}
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			FruitsLinkageParameter p = parameter as FruitsLinkageParameter;

			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 計算式作成（指定單個運算符實例）
				MarkFormulaList(p, () => { return p.Signs[0]; });
			}
			else
			{
				// 計算式作成（加減乘除運算符實例隨機抽取）
				MarkFormulaList(p, () => { return p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]; });
			}

			// 水果連連看對象實例
			FruitsLinkageFormula fruitsLinkageFormula = new FruitsLinkageFormula
			{
				// 左邊水果算式
				FruitsFormulas = _fruitsFormulas,
				// 右側容器算式
				ContainersFormulas = _containersFormulas,
				// 座位號隨機排序
				Sort = _seats.OrderBy(x => Guid.NewGuid()).ToList()
			};
			// 容器的擺放位置
			fruitsLinkageFormula.Seats = GetNewSeats(fruitsLinkageFormula.Sort);

			// 結果設定
			p.Formulas = fruitsLinkageFormula;
		}

		/// <summary>
		/// 右側計算式集合作成并返回當前新作成的計算式
		/// </summary>
		/// <param name="rightFormulas">右側計算式集合</param>
		/// <param name="maximumLimit">計算結果最大值</param>
		/// <param name="leftFormulaAnswer">左側新作成計算式的結果值</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MakeRightFormulas(IList<Formula> rightFormulas, int maximumLimit, int leftFormulaAnswer, Func<SignOfOperation> signFunc)
		{
			IArithmetic strategy = CalculateManager(signFunc());

			// 計算式作成（依據左邊算式的答案推算右邊的算式）
			Formula formula = strategy.CreateFormulaWithAnswer(new ArithmeticParameter()
			{
				MaximumLimit = maximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			}, leftFormulaAnswer);
			rightFormulas.Add(formula);
		}

		/// <summary>
		/// 左側計算式集合作成并返回當前新作成的計算式
		/// </summary>
		/// <param name="leftFormulas">左側計算式集合</param>
		/// <param name="maximumLimit">計算結果最大值</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>新作成的計算式</returns>
		private Formula MakeLeftFormulas(IList<Formula> leftFormulas, int maximumLimit, Func<SignOfOperation> signFunc)
		{
			IArithmetic strategy = CalculateManager(signFunc());

			// 計算式作成
			Formula formula = strategy.CreateFormula(new ArithmeticParameter()
			{
				MaximumLimit = maximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			});
			leftFormulas.Add(formula);

			return formula;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="seats"></param>
		/// <returns></returns>
		private IList<int> GetNewSeats(IList<int> seats)
		{
			IList<int> newSeats = new List<int>();
			for (int number = 0; number < seats.Count; number++)
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
		/// 情況4：算式中有0的出現
		/// </remarks>
		/// <param name="fruitsFormulas">左側水果計算式集合</param>
		/// <param name="containersFormulas"></param>
		/// <param name="answer">計算結果值</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<Formula> fruitsFormulas, IList<Formula> containersFormulas, int answer)
		{
			// 情況4：算式中有0的出現
			if (fruitsFormulas.Last().LeftParameter == 0 || fruitsFormulas.Last().RightParameter == 0
				|| containersFormulas.Last().LeftParameter == 0 || containersFormulas.Last().RightParameter == 0)
			{
				return true;
			}
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