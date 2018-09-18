using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 水果連連看參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.MathWordProblems)]
	public class MathWordProblemsParameter : ParameterBase
	{
		/// <summary>
		/// 水果連連看作成并輸出
		/// </summary>
		public List<MathWordProblemsFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 集合實例化
			Formulas = new List<MathWordProblemsFormula>();
		}
	}
}
