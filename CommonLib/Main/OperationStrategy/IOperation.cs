using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public interface IOperation
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		void MarkFormulaList(ParameterBase parameter);
	}
}
