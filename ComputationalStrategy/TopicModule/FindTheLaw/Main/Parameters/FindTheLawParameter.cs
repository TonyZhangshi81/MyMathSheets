using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters
{
	/// <summary>
	/// 找規律參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.FindTheLaw)]
	public class FindTheLawParameter : ParameterBase
	{
		/// <summary>
		/// 找規律作成并輸出
		/// </summary>
		public IList<FindTheLawFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 找規律集合實例化
			Formulas = new List<FindTheLawFormula>();
		}
	}
}
