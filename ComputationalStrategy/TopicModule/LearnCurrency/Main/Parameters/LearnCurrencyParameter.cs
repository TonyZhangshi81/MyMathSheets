using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters
{
	/// <summary>
	/// 認識貨幣參數類
	/// </summary>
	[OperationParameter("LearnCurrency")]
	public class LearnCurrencyParameter : TopicParameterBase
	{
		/// <summary>
		/// 認識貨幣作成并輸出
		/// </summary>
		public IList<LearnCurrencyFormula> Formulas { get; set; }

		/// <summary>
		/// 認識貨幣題型參數設置
		/// </summary>
		public int[] Types { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			object value = JsonExtension.GetPropertyByJson(Reserve, "Type");
			Types = Convert.ToString(value).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();

			// 認識貨幣集合實例化
			Formulas = new List<LearnCurrencyFormula>();
		}
	}
}