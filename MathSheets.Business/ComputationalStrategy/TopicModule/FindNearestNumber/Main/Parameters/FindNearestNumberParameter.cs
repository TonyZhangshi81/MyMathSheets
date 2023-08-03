using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters
{
	/// <summary>
	/// 尋找最近的數字參數類
	/// </summary>
	[TopicParameter("FindNearestNumber")]
	public class FindNearestNumberParameter : TopicParameterBase
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
			// 集合實例化
			Formulas = new List<NearestNumberFormula>();
		}
	}
}