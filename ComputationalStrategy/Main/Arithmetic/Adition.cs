using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 加法計算式
	/// </summary>
	[Calculate(SignOfOperation.Plus)]
	public class Adition : CalculateBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		/// <remarks>如果未設定最大值則依據指定範圍進行推算</remarks>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			_formula = base.CreateFormula(parameter);

			_formula.Sign = SignOfOperation.Plus;
			if (parameter.MaximumLimit == 0)
			{
				_formula.LeftParameter = GetParameterWithScope(parameter.LeftScope);
				_formula.RightParameter = GetParameterWithScope(parameter.RightScope);
			}
			else
			{
				_formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
				_formula.RightParameter = GetRightParameter(parameter.MaximumLimit, _formula.LeftParameter);
			}
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
			_formula = base.CreateFormula(parameter, previousFormula);

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
			_formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(parameter.MaximumLimit) : previousFormula.Answer;
			_formula.Sign = SignOfOperation.Plus;
			_formula.RightParameter = GetRightParameter(parameter.MaximumLimit, _formula.LeftParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

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
			// 加法运算的最大值就是算式的答案Answer值
			_formula = base.CreateFormulaWithAnswer(parameter, answer);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Plus;
			_formula.LeftParameter = GetLeftParameter(answer);
			_formula.RightParameter = _formula.Answer - _formula.LeftParameter;

			return _formula;
		}
	}
}
