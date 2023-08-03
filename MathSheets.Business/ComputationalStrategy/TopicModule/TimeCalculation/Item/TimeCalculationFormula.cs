using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.ComputationalStrategy.TimeCalculation.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class TimeCalculationFormula
	{
		/// <summary>
		/// 計算開始時間
		/// </summary>
		public TimeType StartTime { get; set; }

		/// <summary>
		/// 計算結束時間
		/// </summary>
		public TimeType EndTime { get; set; }

		/// <summary>
		/// 經過的時間
		/// </summary>
		public TimeType ElapsedTime { get; set; }

		/// <summary>
		/// 运算符
		/// </summary>
		/// <see cref="SignOfOperation"/>
		public SignOfOperation Sign { get; set; }

		/// <summary>
		/// 填空随机项目（运算符左边参数、运算符右边参数、等式结果）
		/// </summary>
		public GapFilling Gap { get; set; }
	}
}