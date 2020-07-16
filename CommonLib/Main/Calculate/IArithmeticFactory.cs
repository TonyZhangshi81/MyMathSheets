using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	/// 運算符對象生產工廠接口類
	/// </summary>
	public interface IArithmeticFactory
	{
		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		IArithmetic GetFormulaOperator(SignOfOperation sign);
	}
}