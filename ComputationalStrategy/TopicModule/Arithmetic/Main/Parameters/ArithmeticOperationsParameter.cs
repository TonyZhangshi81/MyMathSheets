using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Item;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.ArithmeticOperations.Main.Parameters
{
	/// <summary>
	/// 四則運算參數類
	/// </summary>
	[TopicParameter("ArithmeticOperations")]
	public class ArithmeticOperationsParameter : TopicParameterBase
	{
		/// <summary>
		/// 四則運算作成并輸出
		/// </summary>
		public IList<ArithmeticOperationsFormula> Formulas { get; set; }

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
			AnswerIsRight = Convert.ToBoolean(Reserve.GetPropertyByJson("AnswerIsRight"));

			// 四則運算結合實例化
			Formulas = new List<ArithmeticOperationsFormula>();
		}
	}
}