using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main.Operation
{
	/// <summary>
	/// 
	/// </summary>
	public class MathWordProblems : SetThemeBase<List<MathWordProblemsFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType">四则运算类型（标准、随机出题）</param>
		/// <param name="signs">在四则运算标准题下指定运算法（加减乘除）</param>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public MathWordProblems(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, int maximumLimit, int numberOfQuestions) :
			base(maximumLimit, numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
		}

		public override void MarkFormulaList()
		{
			if (_fourOperationsType == FourOperationsType.Default)
			{
				return;
			}
		}
	}
}
