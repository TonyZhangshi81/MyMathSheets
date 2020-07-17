using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Item;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters;
using System;

namespace MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Strategy
{
	/// <summary>
	/// 找最相近的数字題型構築
	/// </summary>
	[Topic("FindNearestNumber")]
	public class FindNearestNumber : TopicBase
	{
		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns></returns>
		private bool MarkFormulaList(FindNearestNumberParameter p, Func<SignOfOperation> signFunc)
		{
			// 关系運算式的左側計算式（指定單個運算符實例）
			Formula leftFormula = MakeLeftFormula(p.MaximumLimit, signFunc);

			// 關係運算符中隨機抽取一個(只限於大於和小於符號)
			SignOfCompare compare = CommonUtil.GetRandomNumber(SignOfCompare.Greater, SignOfCompare.Less);
			// 小於符號推算=》右邊等式結果：左邊等式運算結果加一:左邊等式運算結果減一
			var rightAnswer = (compare == SignOfCompare.Less) ? leftFormula.Answer + 1 : leftFormula.Answer - 1;
			// 判斷是否需要回滾當前算式
			if (CheckIsNeedInverseMethod(compare, leftFormula, p.MaximumLimit))
			{
				return false;
			}

			// 关系運算式的右側計算式（指定單個運算符實例）
			Formula rightFormula = MakeRightFormula(p.MaximumLimit, rightAnswer, signFunc);

			// 在關係運算式中（左側或者右側計算式）隨機確定填空項目所在位置
			bool isLeftFormula = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right) == GapFilling.Left;
			if (isLeftFormula)
			{
				// 在等式左邊的數里隨機選擇一個作為填空項目
				leftFormula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);
			}
			else
			{
				// 在等式右邊的數里隨機選擇一個作為填空項目
				rightFormula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);
			}

			// 計算式作成
			p.Formulas.Add(new NearestNumberFormula()
			{
				LeftFormula = leftFormula,
				RightFormula = rightFormula,
				Answer = compare
			});

			return true;
		}

		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			FindNearestNumberParameter p = parameter as FindNearestNumberParameter;

			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 指定單個運算符題型
					if (!MarkFormulaList(p, () => { return p.Signs[0]; }))
					{
						i--;
						continue;
					}
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 隨機單個運算符題型（加減乘除運算符實例隨機抽取）
					if (!MarkFormulaList(p, () => { return p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]; }))
					{
						i--;
						continue;
					}
				}
			}
		}

		/// <summary>
		/// 左側計算式作成
		/// </summary>
		/// <param name="maximumLimit">計算結果最大值</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>新作成的計算式</returns>
		private Formula MakeLeftFormula(int maximumLimit, Func<SignOfOperation> signFunc)
		{
			IArithmetic strategy = CalculateManager(signFunc());

			// 計算式作成
			Formula formula = strategy.CreateFormula(new ArithmeticParameter()
			{
				MaximumLimit = maximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 1
			});

			return formula;
		}

		/// <summary>
		/// 右側計算式作成
		/// </summary>
		/// <param name="maximumLimit">計算結果最大值</param>
		/// <param name="leftFormulaAnswer">左側新作成計算式的結果值</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>新作成的計算式</returns>
		private Formula MakeRightFormula(int maximumLimit, int leftFormulaAnswer, Func<SignOfOperation> signFunc)
		{
			IArithmetic strategy = CalculateManager(signFunc());

			// 計算式作成（依據左邊算式的答案推算右邊的算式）
			Formula formula = strategy.CreateFormulaWithAnswer(new ArithmeticParameter()
			{
				MaximumLimit = maximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 1
			}, leftFormulaAnswer);

			return formula;
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