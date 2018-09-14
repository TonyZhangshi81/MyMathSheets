using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 運算符自定導出的元數據特性（使用其導出的元數據以挑選需要的對象）
	/// </summary>
	public interface ICalculateMetaDataView
	{
		SignOfOperation Sign { get; }
	}
}
