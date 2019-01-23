using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
using System;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Strategy
{
	/// <summary>
	/// 四則遠算題
	/// </summary>
	[Operation(LayoutSetting.Preview.Arithmetic)]
	public class Arithmetic : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於五次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 5;

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(ArithmeticParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			ICalculate strategy = null;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				strategy = CalculateManager(signFunc());
				Formula formula = strategy.CreateFormula(new CalculateParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = p.QuestionType,
					MinimumLimit = 0
				});
				// 隨機設定是否帶小括號(當參數配置允許小括號)
				formula.IsNeedBracket = p.IsNeedBracket ? CommonUtil.GetRandomNumber(false, true) : false;

				Formula multFormula = null;
				// 是否使用多級運算式
				if (p.Multistage)
				{
					// 第二級計算式作成
					multFormula = GetMultistageFormula(p, formula, () => { return signFunc(); });
				}

				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p, formula, multFormula))
				{
					defeated++;
					// 如果大於五次則認為此題無法作成繼續下一題
					if (defeated == INVERSE_NUMBER)
					{
						// 當前反推判定次數復原
						defeated = 0;
						continue;
					}
					i--;
					continue;
				}

				// 計算式作成
				p.Formulas.Add(new ArithmeticFormula
				{
					// 四則運算式
					Arithmetic = formula,
					// 多級運算式
					MultistageArithmetic = multFormula,
					// 等式值是不是出現在右邊
					AnswerIsRight = IsRight
				});

				defeated = 0;
			}
		}

		/// <summary>
		/// 多級運算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="pervFormula">第一級計算式</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		/// <returns>四則運算式</returns>
		private Formula GetMultistageFormula(ArithmeticParameter p, Formula pervFormula, Func<SignOfOperation> signFunc)
		{
			// 隨機設定是否帶小括號(當參數配置允許小括號且第一級計算式不帶小括號的情況下)
			bool isNeedBracket = (p.IsNeedBracket && !pervFormula.IsNeedBracket) ? CommonUtil.GetRandomNumber(false, true) : false;

			// 隨機運算符取得
			ICalculate strategy = CalculateManager(signFunc());
			// 計算式作成（依據左邊算式的答案推算右邊的算式）
			Formula formula = strategy.CreateFormulaWithAnswer(new CalculateParameter()
			{
				// 第一級計算式如果是減法並且第二級計算式不帶小括號，那麼第二級的結果最大值必須小於或等於第一級計算式中的被減數值
				MaximumLimit = (pervFormula.Sign == SignOfOperation.Subtraction && !isNeedBracket) ? pervFormula.LeftParameter : p.MaximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			}, pervFormula.RightParameter);
			// 隨機設定是否帶小括號
			formula.IsNeedBracket = isNeedBracket;
			// 填空項目設定
			if (pervFormula.Gap == GapFilling.Right)
			{
				formula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);
				pervFormula.Gap = GapFilling.Default;
			}

			return formula;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			ArithmeticParameter p = parameter as ArithmeticParameter;

			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
		}

		/// <summary>
		/// 等式值是不是出現在左邊
		/// </summary>
		public bool IsRight => CommonUtil.GetRandomNumber(LeftOrRight.Left, LeftOrRight.Right) == LeftOrRight.Right;

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：全零的情況
		/// 情況3：多級計算時，第二級計算式結果不能為零
		/// </remarks>
		/// <param name="p">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <param name="multFormula">第二級計算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(ArithmeticParameter p, Formula currentFormula, Formula multFormula)
		{
			// 多級計算時，第二級計算式結果不能為零
			if (p.Multistage)
			{
				if (currentFormula.RightParameter == 0)
				{
					return true;
				}
				// 全零的情況
				if (multFormula.LeftParameter == 0 || multFormula.RightParameter == 0 || multFormula.Answer == 0)
				{
					return true;
				}
			}

			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}

			// 判斷當前算式是否已經出現過
			if (p.Formulas.ToList().Any(d => d.Arithmetic.LeftParameter == currentFormula.LeftParameter
				&& d.Arithmetic.RightParameter == currentFormula.RightParameter
				&& d.Arithmetic.Answer == currentFormula.Answer
				&& d.Arithmetic.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}
	}
}
