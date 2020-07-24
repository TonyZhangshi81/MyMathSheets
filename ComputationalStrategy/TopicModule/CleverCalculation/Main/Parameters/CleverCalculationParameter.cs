using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Parameters
{
	/// <summary>
	/// 巧算參數類
	/// </summary>
	[TopicParameter("CleverCalculation")]
	public class CleverCalculationParameter : TopicParameterBase
	{
		/// <summary>
		/// 等式接龍作成并輸出
		/// </summary>
		public IList<CleverCalculationFormula> Formulas { get; set; }

		/// <summary>
		/// 智能提示
		/// </summary>
		public HelperDialogue BrainpowerHint { get; set; }

		/// <summary>
		/// 題型參數設置
		/// </summary>
		public int[] TopicTypes { get; set; }

		/// <summary>
		/// 子題型參數設置
		/// </summary>
		public int[] SubTopicTypes { get; set; }

		/// <summary>
		/// 初期化參數
		/// </summary>
		public override void InitParameter()
		{
			object topicType = Reserve.GetPropertyByJson("TopicType");
			TopicTypes = Convert.ToString(topicType).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();

			SubTopicTypes = new int[] { };
			if (Reserve.ContainsKey("SubTopicType"))
			{
				object subTopicType = Reserve.GetPropertyByJson("SubTopicType");
				SubTopicTypes = Convert.ToString(subTopicType).Split(new char[] { ',' }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();
			}

			// 巧算集合實例化
			Formulas = new List<CleverCalculationFormula>();
		}
	}
}