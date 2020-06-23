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
			Formula = base.CreateFormula(parameter);

			if (parameter.MaximumLimit == 0)
			{
				Formula.LeftParameter = GetParameterWithScope(parameter.LeftScope);
				Formula.RightParameter = GetParameterWithScope(parameter.RightScope);

			}
			else
			{
				Formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
				Formula.RightParameter = GetRightParameter(Formula.LeftParameter);
			}
			Formula.Sign = SignOfOperation.Subtraction;
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

			if(Formula.Answer < 0)
			{
				// 負數值
				Formula.IsNoSolution = true;
			}

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

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
			Formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(parameter.MaximumLimit) : previousFormula.Answer;
			Formula.Sign = SignOfOperation.Subtraction;
			Formula.RightParameter = GetRightParameter(Formula.LeftParameter);
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

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
			Formula.Sign = SignOfOperation.Subtraction;
			// 计算式左侧项目的取值范围（答案值至最大计算值）
			MinimumLimit = answer;
			Formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
			Formula.RightParameter = Formula.LeftParameter - Formula.Answer;

			return Formula;
		}
	}
}
