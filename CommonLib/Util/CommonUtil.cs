using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 共通函數
	/// </summary>
	public static class CommonUtil
	{
		/// <summary>
		/// 指定範圍內的隨機數取得
		/// </summary>
		/// <param name="upper">上限值（包含）</param>
		/// <param name="lower">下限值（包含）</param>
		/// <returns>隨機數</returns>
		public static int GetRandomNumber(int upper, int lower)
		{
			// 隨機數處理對象
			RandomNumberComposition random = new RandomNumberComposition(upper, lower);
			// 獲取隨機數并返回
			return random.GetRandomNumber();
		}
	}
}
