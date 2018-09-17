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
	public class OperationParameterProviderCollection : ProviderCollection
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public new OperationParameterProvider this[string name] => (OperationParameterProvider)base[name];
	}
}
