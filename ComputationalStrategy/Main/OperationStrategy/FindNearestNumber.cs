using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 找最相近的数字題型構築
	/// </summary>
	[Operation(LayoutSetting.Preview.FindNearestNumber)]
	public class FindNearestNumber : OperationBase
	{
		/// <summary>
		/// 關係運算符中隨機抽取一個(只限於大於和小於符號)
		/// </summary>
		/// <returns></returns>
		private SignOfCompare RandomSignOfCompare
		{
			get
			{
				RandomNumberComposition number = new RandomNumberComposition((int)SignOfCompare.Greater, (int)SignOfCompare.Less);
				return (SignOfCompare)number.GetRandomNumber();
			}
		}

		/// <summary>
		/// 隨機抽取填空項目出現在左邊等式還是右邊等式(只限於左邊和右邊)
		/// </summary>
		/// <returns></returns>
		private GapFilling GetGapFilling
		{
			get
			{
				RandomNumberComposition number = new RandomNumberComposition((int)GapFilling.Left, (int)GapFilling.Right);
				return (GapFilling)number.GetRandomNumber();
			}
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			FindNearestNumberParameter p = parameter as FindNearestNumberParameter;

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 填空項目選邊
					GapFilling gapFilling = GetGapFilling;

					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(p.MaximumLimit, QuestionType.Standard);
					if (gapFilling == GapFilling.Left)
					{
						// 左邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 取得關係運算符
					SignOfCompare randomSign = RandomSignOfCompare;
					// 小於符號推算=》右邊等式結果：左邊等式運算結果加一:左邊等式運算結果減一
					var rightAnswer = (randomSign == SignOfCompare.Less) ? leftFormula.Answer + 1 : leftFormula.Answer - 1;
					// 判斷是否需要回滾當前算式
					if (CheckIsNeedInverseMethod(randomSign, leftFormula, p.MaximumLimit))
					{
						i--;
						continue;
					}

					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormulaWithAnswer(p.MaximumLimit, rightAnswer);
					if (gapFilling == GapFilling.Right)
					{
						// 右邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 計算式作成
					p.Formulas.Add(new EqualityFormula()
					{
						LeftFormula = leftFormula,
						RightFormula = rightFormula,
						Answer = randomSign
					});
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 隨機取得運算式對象
					strategy = GetRandomStrategy(p.Signs);
					// 填空項目選邊
					GapFilling gapFilling = GetGapFilling;

					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(p.MaximumLimit, QuestionType.Standard);
					if (gapFilling == GapFilling.Left)
					{
						// 左邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 取得關係運算符
					SignOfCompare randomSign = RandomSignOfCompare;
					// 小於符號推算=》右邊等式結果：左邊等式運算結果加一:左邊等式運算結果減一
					var rightAnswer = (randomSign == SignOfCompare.Less) ? leftFormula.Answer + 1 : leftFormula.Answer - 1;
					// 判斷是否需要回滾當前算式
					if (CheckIsNeedInverseMethod(randomSign, leftFormula, p.MaximumLimit))
					{
						i--;
						continue;
					}

					// 隨機取得運算式對象
					strategy = GetRandomStrategy(p.Signs);
					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormulaWithAnswer(p.MaximumLimit, rightAnswer);
					if (gapFilling == GapFilling.Right)
					{
						// 左邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 計算式作成
					p.Formulas.Add(new EqualityFormula()
					{
						LeftFormula = leftFormula,
						RightFormula = rightFormula,
						Answer = randomSign
					});
				}
			}
		}

		/// <summary>
		/// 隨機取得運算式對象
		/// </summary>
		/// <param name="signs"></param>
		/// <returns>運算式對象</returns>
		private ICalculate GetRandomStrategy(IList<SignOfOperation> signs)
		{
			RandomNumberComposition random = new RandomNumberComposition((int)SignOfOperation.Plus, signs.Count - 1);
			// 混合題型（加減乘除運算符實例隨機抽取）
			SignOfOperation sign = signs[random.GetRandomNumber()];
			// 對四則運算符實例進行cache管理
			return CalculateManager(sign);
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：左邊等式小於右邊等式時,左邊等式結果已經到達最大計算結果值
		/// 情況2：左邊等式大於右邊等式時,左邊等式結果是0
		/// </remarks>
		/// <param name="sign">左側水果計算式集合</param>
		/// <param name="leftFormula">計算結果值</param>
		/// <param name="maximumLimit"></param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(SignOfCompare sign, Formula leftFormula, int maximumLimit)
		{
			// 情況1
			if (sign == SignOfCompare.Less && leftFormula.Answer == maximumLimit)
			{
				return true;
			}
			// 情況2
			if (sign == SignOfCompare.Greater && leftFormula.Answer == 0)
			{
				return true;
			}
			return false;
		}
	}
}
