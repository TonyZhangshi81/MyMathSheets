using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 減法計算式
	/// </summary>
	[Calculate(SignOfOperation.Subtraction)]
	public class Subtraction : CalculateBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(CalculateParameter parameter)
		{
			_formula = base.CreateFormula(parameter);

			if (parameter.MaximumLimit == 0)
			{
				_formula.LeftParameter = GetParameterWithScope(parameter.LeftScope);
				_formula.RightParameter = GetParameterWithScope(parameter.RightScope);

			}
			else
			{
				_formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
				_formula.RightParameter = GetRightParameter(_formula.LeftParameter);
			}
			_formula.Sign = SignOfOperation.Subtraction;
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);

			if(_formula.Answer < 0)
			{
				// 負數值
				_formula.IsNoSolution = true;
			}

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
			_formula.Sign = SignOfOperation.Subtraction;
			_formula.RightParameter = GetRightParameter(_formula.LeftParameter);
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
			_formula = base.CreateFormulaWithAnswer(parameter, answer);

			_formula.Answer = answer;
			_formula.Sign = SignOfOperation.Subtraction;
			// 计算式左侧项目的取值范围（答案值至最大计算值）
			_minimumLimit = answer;
			_formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
			_formula.RightParameter = _formula.LeftParameter - _formula.Answer;

			return _formula;
		}
	}
}
