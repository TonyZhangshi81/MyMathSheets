using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Item;

namespace MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters
{
	/// <summary>
	/// 水果連連看參數類
	/// </summary>
	[OperationParameter("FruitsLinkage")]
	public class FruitsLinkageParameter : TopicParameterBase
	{
		/// <summary>
		/// 水果連連看作成并輸出
		/// </summary>
		public FruitsLinkageFormula Formulas { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			// 集合實例化
			Formulas = new FruitsLinkageFormula();
		}
	}
}