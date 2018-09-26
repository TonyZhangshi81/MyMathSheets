using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 射門得分題型構築
	/// </summary>
	[Operation(LayoutSetting.Preview.ScoreGoal)]
	public class ScoreGoal : OperationBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			ScoreGoalParameter p = parameter as ScoreGoalParameter;

			ICalculate strategy = null;

			// 射門得分對象實例
			ScoreGoalFormula fruitsLinkageFormula = new ScoreGoalFormula();


		}
	}
}
