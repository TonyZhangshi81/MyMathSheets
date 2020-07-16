using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.BasicOperationsLib.Main.Arithmetic
{
	/// <summary>
	/// 減法計算式
	/// </summary>
	[Arithmetic(SignOfOperation.Subtraction)]
	public class Subtraction : ArithmeticBase
	{
		/// <summary>
		/// 反推判定次數
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 創建計算式
		/// </summary>
		/// <param name="parameter">計算書參數類</param>
		/// <returns>計算式成立: TRUE</returns>
		private bool TryCreateFormula(ArithmeticParameter parameter)
		{
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

			if (Formula.Answer < 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		public override Formula CreateFormula(ArithmeticParameter parameter)
		{
			Formula = base.CreateFormula(parameter);
			// 創建計算式
#pragma warning disable CA1062 // base.CreateFormula已對parameter進行NULL判定處理
			var result = TryCreateFormula(parameter);
#pragma warning restore CA1062

			// 當前反推判定數
			int _defeated = 0;
			while (_defeated < INVERSE_NUMBER)
			{
				if (!result)
				{
					result = TryCreateFormula(parameter);
				}
				else
				{
					break;
				}
				_defeated++;

				if (_defeated == INVERSE_NUMBER)
				{
					Formula.IsNoSolution = true;
				}
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

			// 如果当前是第一层计算式,需要随机获取计算式最左边的值
#pragma warning disable CA1062 // base.CreateFormula已對parameter進行NULL判定處理
			Formula.LeftParameter = (previousFormula == null) ? GetLeftParameter(parameter.MaximumLimit) : previousFormula.Answer;
#pragma warning restore CA1062
			Formula.Sign = SignOfOperation.Subtraction;
			Formula.RightParameter = GetRightParameter(Formula.LeftParameter);
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
			Formula = base.CreateFormulaWithAnswer(parameter, answer);

			Formula.Answer = answer;
			Formula.Sign = SignOfOperation.Subtraction;
			// 计算式左侧项目的取值范围（答案值至最大计算值）
			MinimumLimit = answer;
#pragma warning disable CA1062 // base.CreateFormula已對parameter進行NULL判定處理
			Formula.LeftParameter = GetLeftParameter(parameter.MaximumLimit);
#pragma warning restore CA1062
			Formula.RightParameter = Formula.LeftParameter - Formula.Answer;

			LogUtil.LogCalculate(Formula);

			return Formula;
		}
	}
}