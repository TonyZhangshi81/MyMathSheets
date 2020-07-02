using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.FromProcess.Util
{
	/// <summary>
	/// 自定題型排序類（按照題型的序號進行排序）
	/// </summary>
	public class PreviewToStringComparer : IComparer<string>
	{
		/// <summary>
		/// 方法重寫，將兩個字符串進行比較并返回相應結果
		/// </summary>
		/// <param name="x">字符串</param>
		/// <param name="y">字符串</param>
		/// <returns>比較結果</returns>
		public int Compare(string x, string y)
		{
			if ((int)Enum.Parse(typeof(LayoutSetting.Preview), x) > (int)Enum.Parse(typeof(LayoutSetting.Preview), y))
			{
				return 1;
			}
			else if ((int)Enum.Parse(typeof(LayoutSetting.Preview), x) < (int)Enum.Parse(typeof(LayoutSetting.Preview), y))
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