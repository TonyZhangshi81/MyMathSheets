using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item;
using System;

namespace MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters
{
	/// <summary>
	/// 認識價格參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.CurrencyLinkage)]
	public class CurrencyLinkageParameter : ParameterBase
	{
		/// <summary>
		/// 認識價格作成并輸出
		/// </summary>
		public CurrencyLinkageFormula Formulas { get; set; }
		/// <summary>
		/// 算式個數設置
		/// </summary>
		public int Amount { get; set; }
		/// <summary>
		/// 是否為縱向排列(0:橫向 1:縱向)
		/// </summary>
		public DivQueueType QueueType { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();
			// 答題數量
			Amount = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "Amount"));
			// 是否為縱向排列
			QueueType = (JsonExtension.GetPropertyByJson(Reserve, "DivQueueType") == null) ? DivQueueType.Lengthways : (DivQueueType)Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "DivQueueType"));

			// 集合實例化
			Formulas = new CurrencyLinkageFormula();
		}
	}
}
