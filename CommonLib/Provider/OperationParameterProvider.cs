using MyMathSheets.CommonLib.Main.OperationStrategy;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Provider
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class OperationParameterProvider
	{
		/// <summary>
		/// 共通參數
		/// </summary>
		public object Argument { get; set; }

		/// <summary>
		/// 獲得Parameter
		/// </summary>
		/// <param name="identifier"></param>
		/// <returns></returns>
		public abstract ParameterBase Initialize(string identifier);
	}
}
