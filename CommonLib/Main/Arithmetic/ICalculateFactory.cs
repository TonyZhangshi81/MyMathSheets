using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 運算符對象生產工廠接口類
	/// </summary>
	public interface ICalculateFactory
	{
		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		ICalculate CreateCalculateInstance(SignOfOperation sign);
	}
}
