using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalStrategy.Item;

namespace ComputationalStrategy.Main
{
	/// <summary>
	/// 等式大小比较
	/// </summary>
	public class EqualityComparisonOperation : SetThemeOperationBase<List<string>>
	{
		/// <summary>
		/// 
		/// </summary>
		public override void MarkFormulaList()
		{
			_formulas.Add("1");
		}
	}
}
