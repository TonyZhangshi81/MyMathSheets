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
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			ScoreGoalParameter p = parameter as ScoreGoalParameter;

			ICalculate strategy = null;

			// 射門得分對象實例
			ScoreGoalFormula fruitsLinkageFormula = new ScoreGoalFormula();

			// 球類算式實例
			IList<Formula> ballsFormulas = new List<Formula>();
			// 球門算式實例
			IList<Formula> goalsFormulas = new List<Formula>();
			// 容器座位號
			IList<int> seats = new List<int>();

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
					if (CheckIsNeedInverseMethod(goal, goalsFormulas))
					{
						i--;
						continue;
					}
					goalsFormulas.Add(goal);
				}
				// 按照指定數量作成相應的數學計算式(足球的個數最多10個)
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 選取球門
					int answer = GetGoal(goalsFormulas);
					// 足球计算式
					Formula ballFormula = strategy.CreateFormulaWithAnswer(p.MaximumLimit, answer);

					
				}
			}
			else
			{
			}
		}

		/// <summary>
		/// 選取球門
		/// </summary>
		/// <returns>被選擇的計算結果</returns>
		private int GetGoal(IList<Formula> goalsFormulas)
		{
			// 在兩個球門之間隨機選擇
			RandomNumberComposition random = new RandomNumberComposition(0, 2);
			return goalsFormulas[random.GetRandomNumber()].Answer;
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：計算式中三個數值都為零
		/// 情況3：答案一致的兩個球門不允許
		/// </remarks>
		/// <param name="currentFormula">球門A</param>
		/// <param name="goalsFormulaB">球門B</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(Formula currentFormula, IList<Formula> goalsFormulas)
		{
			// 等式左邊參數都是零的情況
			if(currentFormula.RightParameter ==0 && currentFormula.LeftParameter == 0)
			{
				return true;
			}

			// 兩個球門算式
			if(goalsFormulas.Count() == 2)
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
