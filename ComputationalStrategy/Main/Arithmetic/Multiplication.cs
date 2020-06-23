using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 乘法計算式
	/// </summary>
	[Calculate(SignOfOperation.Multiple)]
	public class Multiplication : CalculateBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			Formula = base.CreateFormula(parameter);

			Formula.LeftParameter = GetLeftParameter(9);
			Formula.Sign = SignOfOperation.Multiple;
			Formula.RightParameter = GetRightParameter(9);
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);
			// 结果特殊处理（在乘法式中其中一个数值为零，那另一个值可以是任意一个数值）
			ResultSpecialHandling();

			return Formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter, Formula previousFormula)
		{
			Formula = base.CreateFormula(parameter, previousFormula);

			// TODO

			return Formula;
		}

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(CalculateParameter parameter, int answer)
		{
			Formula = base.CreateFormulaWithAnswer(parameter, answer);

			Formula.Answer = answer;
			Formula.Sign = SignOfOperation.Multiple;
			Formula.LeftParameter = GetLeftParameter(9);
			// 判定是否能被整除
			if(Formula.Answer % Formula.LeftParameter != 0)
			{
				// 無解計算式（結果無法被整除）
				Formula.IsNoSolution = true;
				return Formula;
			}
			Formula.RightParameter = Formula.Answer / Formula.LeftParameter;

			return Formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (Formula.Gap == GapFilling.Left && Formula.RightParameter == 0)
			{
				Formula.LeftParameter = -999;
			}

			if (Formula.Gap == GapFilling.Right && Formula.LeftParameter == 0)
			{
				Formula.RightParameter = -999;
			}
		}
	}
}
