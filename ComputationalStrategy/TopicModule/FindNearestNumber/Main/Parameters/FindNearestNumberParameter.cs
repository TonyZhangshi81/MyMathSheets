using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters
{
	/// <summary>
	/// 尋找最近的數字參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.FindNearestNumber, "FN001|FN002|FN003")]
	public class FindNearestNumberParameter : ParameterBase
	{
		/// <summary>
		/// 尋找最近的數字作成并輸出
		/// </summary>
		public IList<NearestNumberFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 集合實例化
			Formulas = new List<NearestNumberFormula>();
		}
	}
}
