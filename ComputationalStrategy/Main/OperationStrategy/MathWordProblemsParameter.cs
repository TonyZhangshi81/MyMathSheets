using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 應用題參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.MathWordProblems, "MP001|MP002|MP003")]
	public class MathWordProblemsParameter : ParameterBase
	{
		/// <summary>
		/// 應用題作成并輸出
		/// </summary>
		public IList<MathWordProblemsFormula> Formulas { get; set; }
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
