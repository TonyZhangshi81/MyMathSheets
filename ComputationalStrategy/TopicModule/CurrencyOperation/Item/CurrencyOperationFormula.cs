using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.CurrencyOperation.Item
{
	/// <summary>
	/// 題型計算式對象構成
	/// </summary>
	public class CurrencyOperationFormula
	{
		/// <summary>
		/// 貨幣運算方程式
		/// </summary>
		public Formula CurrencyArithmetic { get; set; }
		/// <summary>
		/// 計算式左右兩側是否互換位置
		/// </summary>
		public bool AnswerIsRight { get; set; }
	}
}
