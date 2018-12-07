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
			_formula = base.CreateFormula(parameter);

			_formula.RightParameter = GetLeftParameter(9);
			_formula.Sign = SignOfOperation.Division;
			_formula.LeftParameter = GetRightParameter(9, _formula.RightParameter);
			_formula.Answer = GetAnswer(_formula.LeftParameter, _formula.RightParameter);
			// 结果特殊处理(当被除数为0时,求解值可以为任何数)  只在随机除法填空题型且分子为0的情况下
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
			_formula.Sign = SignOfOperation.Division;
			_formula.LeftParameter = GetLeftParameter(9);
			// 判定是否超出九九乘法口訣上限值
			if (_formula.Answer > 81)
			{
				// 無解計算式（結果無法被整除）
				_formula.IsNoSolution = true;
				return _formula;
			}
			_formula.RightParameter = _formula.Answer * _formula.LeftParameter;

			return _formula;
		}

		/// <summary>
		/// 
		/// </summary>
		private void ResultSpecialHandling()
		{
			if (_formula.Gap == GapFilling.Right && _formula.RightParameter == 0)
			{
				_formula.RightParameter = -999;
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
			var number = CommonUtil.GetRandomNumber(_minimumLimit, maximumLimit);
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
