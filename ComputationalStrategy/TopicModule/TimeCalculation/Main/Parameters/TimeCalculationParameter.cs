using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters
{
	/// <summary>
	/// 時間運算參數類
	/// </summary>
	[OperationParameter("TimeCalculation")]
	public class TimeCalculationParameter : ParameterBase
	{
		/// <summary>
		/// 時間運算題型參數
		/// </summary>
		public IList<TimeCalculationFormula> Formulas { get; set; }

		/// <summary>
		/// 秒數是否清零
		/// </summary>
		public bool IsShowSeconds { get; set; }

		/// <summary>
		/// 參數設置(間隔小時數的取值範圍)
		/// </summary>
		public int[] ElapsedHours { get; set; }

		/// <summary>
		/// 是否指定分針
		/// 0：指定分針(以15分鐘為單位隨機抽取)  1：隨機取值
		/// </summary>
		public bool IsEssignMinutes { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 0：清零  1：不清零(保留隨機)
			IsShowSeconds = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "IsShowSeconds")) == 0;

			// 0：指定分針(以15分鐘為單位隨機抽取)  1：隨機取值
			IsEssignMinutes = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "IsEssignMinutes")) == 0;

			// 經過小時數的取值範圍
			object value = JsonExtension.GetPropertyByJson(Reserve, "ElapsedHours");
			ElapsedHours = Convert.ToString(value).Split(',').Select(s => int.Parse(s)).ToArray();

			// 時間運算集合實例化
			Formulas = new List<TimeCalculationFormula>();
		}
	}
}