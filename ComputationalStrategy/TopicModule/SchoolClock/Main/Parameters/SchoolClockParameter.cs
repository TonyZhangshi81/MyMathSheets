using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters
{
	/// <summary>
	/// 時鐘學習板參數類
	/// </summary>
	[OperationParameter(LayoutSetting.Preview.SchoolClock)]
	public class SchoolClockParameter : ParameterBase
	{
		/// <summary>
		/// 時鐘學習板題型參數
		/// </summary>
		public IList<SchoolClockFormula> Formulas { get; set; }
		/// <summary>
		/// 秒數是否清零
		/// </summary>
		public bool IsShowSeconds { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			base.InitParameter();

			// 0：清零  1：不清零(保留隨機)
			IsShowSeconds = Convert.ToInt32(JsonExtension.GetPropertyByJson(Reserve, "IsShowSeconds")) == 0;

			// 時鐘學習板集合實例化
			Formulas = new List<SchoolClockFormula>();
		}
	}
}
