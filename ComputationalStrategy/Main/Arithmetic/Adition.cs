using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Calculate.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 加法計算式
	/// </summary>
	[Arithmetic(SignOfOperation.Plus)]
	public class Adition : ArithmeticBase
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		/// <remarks>如果未設定最大值則依據指定範圍進行推算</remarks>
		public override Formula CreateFormula(ArithmeticParameter parameter)
		{
			Formula = base.CreateFormula(parameter);

			Formula.Sign = SignOfOperation.Plus;

			if (parameter != null)
			{
				if (parameter.MaximumLimit == 0)
				{
					Formula.LeftParameter = GetParameterWithScope(parameter.LeftScope);
					Formula.RightParameter = GetParameterWithScope(parameter.RightScope);
				}
				else
				{
					Formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
					Formula.RightParameter = GetRightParameter(parameter.MaximumLimit, Formula.LeftParameter);
				}
			}

			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

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

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
#pragma warning disable CA1062 // base.CreateFormula已對parameter進行NULL判定處理
			Formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(parameter.MaximumLimit) : previousFormula.Answer;
#pragma warning restore CA1062
			Formula.Sign = SignOfOperation.Plus;
			Formula.RightParameter = GetRightParameter(parameter.MaximumLimit, Formula.LeftParameter);
			Formula.Answer = GetAnswer(Formula.LeftParameter, Formula.RightParameter);

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
			// 加法运算的最大值就是算式的答案Answer值
			Formula = base.CreateFormulaWithAnswer(parameter, answer);

			Formula.Answer = answer;
			Formula.Sign = SignOfOperation.Plus;
			Formula.LeftParameter = GetLeftParameter(answer);
			Formula.RightParameter = Formula.Answer - Formula.LeftParameter;

			LogUtil.LogCalculate(Formula);

			return Formula;
		}
	}
}