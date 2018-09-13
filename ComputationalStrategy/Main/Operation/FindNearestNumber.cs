using MyMathSheets.CommonLib.Main.ArithmeticStrategy;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Operation;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.Operation
{
	/// <summary>
	/// 找最相近的数字題型構築
	/// </summary>
	public class FindNearestNumber : SetThemeBase<List<EqualityFormula>>
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
		/// 找最相近的数字題型構築對象初期化
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="questionType">题型（标准、随机填空）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public FindNearestNumber(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
			: base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
			_questionType = questionType;
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

			ICalculatePattern strategy = null;

			// 標準題型（指定單個運算符）
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				// 指定單個運算符實例
				strategy = GetPatternInstance(_signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 填空項目選邊
					GapFilling gapFilling = GetGapFilling;

					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(_maximumLimit, QuestionType.Standard);
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
					if (CheckIsNeedInverseMethod(randomSign, leftFormula))
					{
						i--;
						continue;
					}

					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormulaWithAnswer(_maximumLimit, rightAnswer);
					if (gapFilling == GapFilling.Right)
					{
						// 右邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 計算式作成
					_formulas.Add(new EqualityFormula()
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
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					// 隨機取得運算式對象
					strategy = GetRandomStrategy();
					// 填空項目選邊
					GapFilling gapFilling = GetGapFilling;

					// 关系符左边计算式
					Formula leftFormula = strategy.CreateFormula(_maximumLimit, QuestionType.Standard);
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
					if (CheckIsNeedInverseMethod(randomSign, leftFormula))
					{
						i--;
						continue;
					}

					// 隨機取得運算式對象
					strategy = GetRandomStrategy();
					// 关系符右边计算式
					Formula rightFormula = strategy.CreateFormulaWithAnswer(_maximumLimit, rightAnswer);
					if (gapFilling == GapFilling.Right)
					{
						// 左邊算式隨機設定填空項目
						strategy.SetGapFillingItem(GapFilling.Left, GapFilling.Right);
					}

					// 計算式作成
					_formulas.Add(new EqualityFormula()
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
		/// <returns>運算式對象</returns>
		private ICalculatePattern GetRandomStrategy()
		{
			RandomNumberComposition random = new RandomNumberComposition((int)SignOfOperation.Plus, _signs.Count - 1);
			// 混合題型（加減乘除運算符實例隨機抽取）
			SignOfOperation sign = _signs[random.GetRandomNumber()];
			// 對四則運算符實例進行cache管理
			return GetPatternInstance(sign);
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：左邊等式小於右邊等式時,左邊等式結果已經到達最大計算結果值
		/// 情況2：左邊等式大於右邊等式時,左邊等式結果是0
		/// </remarks>
		/// <param name="fruitsFormulas">左側水果計算式集合</param>
		/// <param name="answer">計算結果值</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(SignOfCompare sign, Formula leftFormula)
		{
			// 情況1
			if (sign == SignOfCompare.Less && leftFormula.Answer == _maximumLimit)
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
