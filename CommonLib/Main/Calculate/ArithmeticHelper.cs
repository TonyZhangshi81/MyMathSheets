using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	/// 用於運算符實例取得的HEPLER類
	/// </summary>
	public class ArithmeticHelper
	{
		private readonly IArithmeticFactory arithmeticFactory;

		/// <summary>
		/// 實例化
		/// </summary>
		public ArithmeticHelper(IArithmeticFactory factory)
		{
			arithmeticFactory = factory;
		}

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		public IArithmetic CreateArithmeticInstance(SignOfOperation sign)
		{
			// 運算符工廠實例化
			return arithmeticFactory.GetFormulaOperator(sign);
		}
	}
}