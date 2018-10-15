using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters
{
	/// <summary>
	/// 比多少參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.HowMuchMore, "HMM001")]
	public class HowMuchMoreParameter : ParameterBase
	{
		/// <summary>
		/// 比多少作成并輸出
		/// </summary>
		public IList<HowMuchMoreFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 集合實例化
			Formulas = new List<HowMuchMoreFormula>();
		}
	}
}
