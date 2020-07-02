using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.FromProcess.Util
{
	/// <summary>
	/// 自定題型排序類（按照題型種類的序號進行排序）
	/// </summary>
	public class ClassifyToIntComparer : IComparer<LayoutSetting.Classify>
	{
		/// <summary>
		/// 方法重寫，將兩個字符串進行比較并返回相應結果
		/// </summary>
		/// <param name="x">字符串</param>
		/// <param name="y">字符串</param>
		/// <returns>比較結果</returns>
		public int Compare(LayoutSetting.Classify x, LayoutSetting.Classify y)
		{
			if ((int)x > (int)y)
			{
				return 1;
			}
			else if ((int)x < (int)y)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}
	}
}