using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 等式大小比较參數類
	/// </summary>
	public class FindNearestNumberParameter : ParameterBase
	{
		/// <summary>
		/// 等式大小比较作成并輸出
		/// </summary>
		public IList<EqualityFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 集合實例化
			Formulas = new List<EqualityFormula>();
		}
	}
}
