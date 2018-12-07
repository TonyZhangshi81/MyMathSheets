using System;

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
		public static T GetRandomNumber<T>(T upper, T lower)
		{
			// 隨機數處理對象
			RandomNumberComposition random = new RandomNumberComposition(Convert.ToInt32(upper), Convert.ToInt32(lower));
			// 獲取隨機數并返回
			return (T)ConvertHelper.ChangeType(random.GetRandomNumber(), typeof(T));
		}
	}
}
