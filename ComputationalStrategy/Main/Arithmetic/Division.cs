using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 除法計算式
	/// </summary>
	[Calculate(SignOfOperation.Division)]
	public class Division : CalculateBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			Formula = base.CreateFormula(parameter);

			Formula.RightParameter = GetLeftParameter(9);
			Formula.Sign = SignOfOperation.Division;
			Formula.LeftParameter = GetRightParameter(9, Formula.RightParameter);
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);
			// 结果特殊处理(当被除数为0时,求解值可以为任何数)  只在随机除法填空题型且分子为0的情况下
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
			Formula.Sign = SignOfOperation.Division;
			Formula.LeftParameter = GetLeftParameter(9);
			// 判定是否超出九九乘法口訣上限值
			if (Formula.Answer > 81)
			{
				// 無解計算式（結果無法被整除）
				Formula.IsNoSolution = true;
				return Formula;
			}
			Formula.RightParameter = Formula.Answer * Formula.LeftParameter;

			return Formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (Formula.Gap == GapFilling.Right && Formula.RightParameter == 0)
			{
				Formula.RightParameter = -999;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="rightParameter"></param>
		/// <returns></returns>
		protected override int GetRightParameter(int maximumLimit, int rightParameter)
		{
			var number = CommonUtil.GetRandomNumber(MinimumLimit, maximumLimit);
			return number * rightParameter;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		protected override int GetLeftParameter(int maximumLimit)
		{
			var number = CommonUtil.GetRandomNumber(1, maximumLimit);
			return number;
		}
	}
}
