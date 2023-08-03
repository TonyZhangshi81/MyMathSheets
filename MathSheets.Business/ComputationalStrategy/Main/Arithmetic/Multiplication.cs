using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Calculate.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 乘法計算式
	/// </summary>
	[Arithmetic(SignOfOperation.Multiple)]
	public class Multiplication : ArithmeticBase
	{
		/// <summary>
		/// 反推判定次數
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(ArithmeticParameter parameter)
		{
			Formula = base.CreateFormula(parameter);

			Formula.LeftParameter = GetLeftParameter(9);
			Formula.Sign = SignOfOperation.Multiple;
			Formula.RightParameter = GetRightParameter(9);
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

			// 结果特殊处理（在乘法式中其中一个数值为零，那另一个值可以是任意一个数值）
			if (Formula.Gap == GapFilling.Left && Formula.RightParameter == 0)
			{
				Formula.LeftParameter = -999;
			}

			if (Formula.Gap == GapFilling.Right && Formula.LeftParameter == 0)
			{
				Formula.RightParameter = -999;
			}

			LogUtil.LogCalculate(Formula);

			return Formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(ArithmeticParameter parameter, Formula previousFormula)
		{
			Formula = base.CreateFormula(parameter, previousFormula);

			// TODO

			LogUtil.LogCalculate(Formula);

			return Formula;
		}

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(ArithmeticParameter parameter, int answer)
		{
			Formula = base.CreateFormulaWithAnswer(parameter, answer);
			// 創建計算式
			var result = TryCreateFormulaWithAnswer(answer);

			// 當前反推判定數
			int _defeated = 0;
			while (_defeated < INVERSE_NUMBER)
			{
				if (!result)
				{
					result = TryCreateFormulaWithAnswer(answer);
				}
				else
				{
					break;
				}
				_defeated++;

				if (_defeated == INVERSE_NUMBER)
				{
					// 無解計算式（結果無法被整除）
					Formula.IsNoSolution = true;
				}
			}

			LogUtil.LogCalculate(Formula);

			return Formula;
		}

		/// <summary>
		/// 創建計算式
		/// </summary>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式成立: TRUE</returns>
		private bool TryCreateFormulaWithAnswer(int answer)
		{
			Formula.Answer = answer;
			Formula.Sign = SignOfOperation.Multiple;
			Formula.LeftParameter = GetLeftParameter(9);
			// 判定是否能被整除
			if (Formula.Answer % Formula.LeftParameter != 0)
			{
				return false;
			}
			Formula.RightParameter = Formula.Answer / Formula.LeftParameter;

			return true;
		}
	}
}