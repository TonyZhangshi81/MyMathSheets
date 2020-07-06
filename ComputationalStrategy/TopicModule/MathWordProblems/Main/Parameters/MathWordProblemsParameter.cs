using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters
{
	/// <summary>
	/// 應用題參數類
	/// </summary>
	[OperationParameter("MathWordProblems")]
	public class MathWordProblemsParameter : ParameterBase
	{
		/// <summary>
		/// 應用題作成并輸出
		/// </summary>
		public IList<MathWordProblemsFormula> Formulas { get; set; }

		/// <summary>
		/// 基礎填空題型參數設置（級別選擇）
		/// </summary>
		public int[] Levels { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			object value = JsonExtension.GetPropertyByJson(Reserve, "Level");
			Levels = Convert.ToString(value).Split(',').Select(s => int.Parse(s)).ToArray();

			// 集合實例化
			Formulas = new List<MathWordProblemsFormula>();
		}
	}
}