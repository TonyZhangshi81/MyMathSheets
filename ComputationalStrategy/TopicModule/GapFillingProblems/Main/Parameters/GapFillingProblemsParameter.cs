using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters
{
	/// <summary>
	/// 填空題參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.GapFillingProblems)]
	public class GapFillingProblemsParameter : ParameterBase
	{
		/// <summary>
		/// 填空題作成并輸出
		/// </summary>
		public IList<GapFillingProblemsFormula> Formulas { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 集合實例化
			Formulas = new List<GapFillingProblemsFormula>();
		}
	}
}
