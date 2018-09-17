using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 計算式策略對象生產工廠接口類
	/// </summary>
	public interface IOperationFactory
	{
		/// <summary>
		/// 對指定計算式策略實例化
		/// </summary>
		/// <param name="preview">策略種類</param>
		/// <returns>策略實例</returns>
		IOperation CreateOperationInstance(LayoutSetting.Preview preview);
	}
}
