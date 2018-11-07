using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 自定導出的元數據特性
	/// </summary>
	public interface IParameterProviderMetaDataView
	{
		/// <summary>
		/// Provider名
		/// </summary>
		string Name { get; }
	}
}
