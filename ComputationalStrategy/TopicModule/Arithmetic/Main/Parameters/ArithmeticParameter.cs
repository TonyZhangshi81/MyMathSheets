using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters
{
	/// <summary>
	/// 四則運算參數類
	/// </summary>
	[OperationParameter("Arithmetic")]
	public class ArithmeticParameter : ParameterBase
	{
		/// <summary>
		/// 四則運算作成并輸出
		/// </summary>
		public IList<ArithmeticFormula> Formulas { get; set; }

		/// <summary>
		/// 智能提示
		/// </summary>
		public HelperDialogue BrainpowerHint { get; set; }

		/// <summary>
		/// 等式值是不是出現在右邊
		/// </summary>
		public bool? AnswerIsRight { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			AnswerIsRight = Convert.ToBoolean(Reserve.GetPropertyByJson("AnswerIsRight"));

			// 四則運算結合實例化
			Formulas = new List<ArithmeticFormula>();
		}
	}
}