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
	public abstract class OperationParameterProvider : ProviderBase
	{
		/// <summary>
		/// 获得Parameter
		/// </summary>
		/// <param name="parameter"></param>
		/// <param name="identifier"></param>
		/// <returns></returns>
		public abstract void Set(ParameterBase parameter, string identifier);
	}
}
