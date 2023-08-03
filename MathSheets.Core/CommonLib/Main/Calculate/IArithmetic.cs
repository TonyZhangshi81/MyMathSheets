using MyMathSheets.CommonLib.Main.Item;

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	/// 計算式作成接口類
	/// </summary>
	public interface IArithmetic
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <returns>計算式對象</returns>
		Formula CreateFormula(ArithmeticParameter parameter);

		/// <summary>
		/// 構造用於計算接龍題型(即：計算式左邊值等於上一個計算式的結果值)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="previousFormula">前次推算的計算式對象</param>
		/// <returns>計算式對象</returns>
		Formula CreateFormula(ArithmeticParameter parameter, Formula previousFormula);

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="parameter">計算式參數類</param>
		/// <param name="answer">計算結果</param>
		/// <returns>計算式對象</returns>
		Formula CreateFormulaWithAnswer(ArithmeticParameter parameter, int answer);
	}
}