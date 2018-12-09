using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.TimeFlies
{
	/// <summary>
	/// 過去的時間
	/// </summary>
	[Calculate(SignOfOperation.Before)]
	public class Past : CalculateBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			_formula = base.CreateFormula(parameter);

			_formula.LeftParameter = parameter.MaximumLimit;
			_formula.Sign = SignOfOperation.Subtraction;
			_formula.RightParameter = parameter.MinimumLimit;
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			return _formula;
		}

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter, Formula previousFormula)
		{
			return CreateFormula(parameter);
		}

		/// <summary>
		/// 由計算結果推算出計算式
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(CalculateParameter parameter, int answer)
		{
			return CreateFormula(parameter);
		}
	}
}
