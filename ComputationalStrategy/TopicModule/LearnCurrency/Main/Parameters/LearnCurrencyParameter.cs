using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters
{
	/// <summary>
	/// 認識貨幣參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.LearnCurrency)]
	public class LearnCurrencyParameter : ParameterBase
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
			base.InitParameter();

			object value = JsonExtension.GetPropertyByJson(Reserve, "Type");
			Types = Convert.ToString(value).Split(',').Select(s => int.Parse(s)).ToArray();

			// 認識貨幣集合實例化
			Formulas = new List<LearnCurrencyFormula>();
		}
	}
}
