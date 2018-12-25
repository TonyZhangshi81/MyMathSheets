using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item
{
	/// <summary>
	/// 題型計算式對象構成
	/// </summary>
	public class LearnLengthUnitFormula
	{
		/// <summary>
		/// 長度單位
		/// </summary>
		public LengthUnit LengthUnitItme { get; set; }
		/// <summary>
		/// 填空項目種類(左邊或者右邊為填空項目)
		/// </summary>
		public GapFilling Gap { get; set; }
		/// <summary>
		/// 剩下的毫米
		/// </summary>
		public int? RemainderMillimeter { get; set; }
		/// <summary>
		/// 剩下的釐米
		/// </summary>
		public int? RemainderCentimeter { get; set; }
		/// <summary>
		/// 剩下的分米
		/// </summary>
		public int? RemainderDecimetre { get; set; }

		/// <summary>
		/// 長度轉換題型種類
		/// </summary>
		public LengthUnitTransform LengthUnitTransformType { get; set; }
	}
}
