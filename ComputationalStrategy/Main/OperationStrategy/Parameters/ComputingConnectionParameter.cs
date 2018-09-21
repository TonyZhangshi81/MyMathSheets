using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameter
{
	/// <summary>
	/// 等式接龍參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.ComputingConnection, "CC001|CC002|CC003")]
	public class ComputingConnectionParameter : ParameterBase
	{
		/// <summary>
		/// 等式接龍作成并輸出
		/// </summary>
		public IList<ConnectionFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 等式接龍集合實例化
			Formulas = new List<ConnectionFormula>();
		}
	}
}
