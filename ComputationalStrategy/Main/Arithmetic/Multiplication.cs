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
			_formula = base.CreateFormula(parameter);

			_formula.LeftParameter = GetLeftParameter(9);
			_formula.Sign = SignOfOperation.Multiple;
			_formula.RightParameter = GetRightParameter(9);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理（在乘法式中其中一个数值为零，那另一个值可以是任意一个数值）
			ResultSpecialHandling();

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
			_formula = base.CreateFormula(parameter, previousFormula);

			// TODO

			return _formula;
		}

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormulaWithAnswer(CalculateParameter parameter, int answer)
		{
			_formula = base.CreateFormulaWithAnswer(parameter, answer);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Multiple;
			_formula.LeftParameter = GetLeftParameter(9);
			// 判定是否能被整除
			if(_formula.Answer % _formula.LeftParameter != 0)
			{
				// 無解計算式（結果無法被整除）
				_formula.IsNoSolution = true;
				return _formula;
			}
			_formula.RightParameter = _formula.Answer / _formula.LeftParameter;

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (_formula.Gap == GapFilling.Left && _formula.RightParameter == 0)
			{
				_formula.LeftParameter = -999;
			}

			if (_formula.Gap == GapFilling.Right && _formula.LeftParameter == 0)
			{
				_formula.RightParameter = -999;
			}
		}
	}
}
