using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Item;
using System;

namespace MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters
{
	/// <summary>
	/// 算式連一連參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.EqualityLinkage)]
	public class EqualityLinkageParameter : ParameterBase
	{
		/// <summary>
		/// 算式連一連作成并輸出
		/// </summary>
		public EqualityLinkageFormula Formulas { get; set; }
		/// <summary>
		/// 算式個數設置
		/// </summary>
		public int Amount { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			Amount = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "Amount"));

			// 集合實例化
			Formulas = new EqualityLinkageFormula();
		}
	}
}
