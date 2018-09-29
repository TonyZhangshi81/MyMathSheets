using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 射門得分題型構築
	/// </summary>
	[Operation(LayoutSetting.Preview.ScoreGoal)]
	public class ScoreGoal : OperationBase
	{
		/// <summary>
		/// 計算式作成處理
		/// </summary>
		/// <param name="parameter">參數</param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			ScoreGoalParameter p = parameter as ScoreGoalParameter;

			ICalculate strategy = null;

			// 射門得分對象實例
			ScoreGoalFormula fruitsLinkageFormula = new ScoreGoalFormula();

			// 球類算式實例
			Dictionary<Formula, int> ballsFormulas = new Dictionary<Formula, int>();
			// 球門算式實例
			IList<Formula> goalsFormulas = new List<Formula>();
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的球門數學計算式(兩個球門)
				for (var i = 0; i < 2; i++)
				{
					// 計算式作成
					Formula goal = strategy.CreateFormula(p.MaximumLimit, QuestionType.Standard, 0);
					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethodForGoals(goal, goalsFormulas))
					{
						i--;
						continue;
					}
					goalsFormulas.Add(goal);
				}
				// 按照指定數量作成相應的數學計算式(足球的個數最多10個)
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					int seat = 0;
					// 選取球門
					int answer = GetGoal(goalsFormulas, ref seat);
					// 足球计算式
					Formula formula = strategy.CreateFormulaWithAnswer(p.MaximumLimit, answer);
					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethodForBalls(formula, ballsFormulas))
					{
						i--;
						continue;
					}
					// 足球計算式
					ballsFormulas.Add(formula, seat);
				}
				p.Formulas.GoalsFormulas = goalsFormulas;
				p.Formulas.BallsFormulas = ballsFormulas;
			}
			else
			{
				RandomNumberComposition random;
				// 按照指定數量作成相應的球門數學計算式(兩個球門)
				for (var i = 0; i < 2; i++)
				{
					random = new RandomNumberComposition(0, p.Signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					SignOfOperation sign = p.Signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager(sign);
					// 計算式作成
					Formula goal = strategy.CreateFormula(p.MaximumLimit, QuestionType.Standard, 0);
					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethodForGoals(goal, goalsFormulas))
					{
						i--;
						continue;
					}
					goalsFormulas.Add(goal);
				}
				// 按照指定數量作成相應的數學計算式(足球的個數最多10個)
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					int seat = 0;
					// 混合題型（加減乘除運算符實例隨機抽取）
					random = new RandomNumberComposition(0, p.Signs.Count - 1);
					SignOfOperation sign = p.Signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager(sign);

					// 隨機挑選球門
					int answer = GetGoal(goalsFormulas, ref seat);
					// 足球计算式
					Formula formula = strategy.CreateFormulaWithAnswer(p.MaximumLimit, answer);
					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethodForBalls(formula, ballsFormulas))
					{
						i--;
						continue;
					}
					// 足球計算式
					ballsFormulas.Add(formula, seat);
				}
				p.Formulas.GoalsFormulas = goalsFormulas;
				p.Formulas.BallsFormulas = ballsFormulas;
			}
		}

		/// <summary>
		/// 選取球門
		/// </summary>
		/// <param name="goalsFormulas">球門算式集合</param>
		/// <param name="seat">球門號(1號球門或者0號球門)</param>
		/// <returns>被選擇的計算結果</returns>
		private int GetGoal(IList<Formula> goalsFormulas, ref int seat)
		{
			// 在兩個球門之間隨機選擇
			RandomNumberComposition random = new RandomNumberComposition(0, 1);
			// 選擇的球門號
			seat = random.GetRandomNumber();
			return goalsFormulas[seat].Answer;
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：計算式中三個數值都為零
		/// 情況3：算式一致的計算式
		/// </remarks>
		/// <param name="currentFormula">當前算式</param>
		/// <param name="ballsFormulas">已有的算式集合</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethodForBalls(Formula currentFormula, Dictionary<Formula, int> ballsFormulas)
		{
			// 等式左邊參數都是零的情況
			if (currentFormula.RightParameter == 0 && currentFormula.LeftParameter == 0)
			{
				return true;
			}
			// 算式存在一致
			if (ballsFormulas.ToList().Any(d => d.Key.RightParameter == currentFormula.RightParameter
											&& d.Key.LeftParameter == currentFormula.LeftParameter
											&& d.Key.Answer == currentFormula.Answer
											&& d.Key.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：計算式中三個數值都為零
		/// 情況3：答案一致的兩個球門不允許
		/// </remarks>
		/// <param name="currentFormula">球門A</param>
		/// <param name="goalsFormulas">球門B</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethodForGoals(Formula currentFormula, IList<Formula> goalsFormulas)
		{
			// 等式左邊參數都是零的情況
			if ((currentFormula.RightParameter == 0 && currentFormula.LeftParameter == 0) || currentFormula.Answer == 0)
			{
				return true;
			}

			// 兩個球門算式
			if (goalsFormulas.Count() == 2)
			{
				// 答案一致的兩個球門不允許
				if (goalsFormulas[0].Answer == currentFormula.Answer)
				{
					return true;
				}
			}
			return false;
		}
	}
}
