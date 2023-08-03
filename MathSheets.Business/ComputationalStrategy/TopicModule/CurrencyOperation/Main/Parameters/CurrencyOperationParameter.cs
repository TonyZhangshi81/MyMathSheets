using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.ComputationalStrategy.CurrencyOperation.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters
{
	/// <summary>
	/// 貨幣運算參數類
	/// </summary>
	[TopicParameter("CurrencyOperation")]
	public class CurrencyOperationParameter : TopicParameterBase
	{
		/// <summary>
		/// 貨幣運算參數作成并輸出
		/// </summary>
		public IList<CurrencyOperationFormula> Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 貨幣運算集合實例化
			Formulas = new List<CurrencyOperationFormula>();
		}
	}
}