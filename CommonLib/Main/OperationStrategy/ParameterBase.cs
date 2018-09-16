using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class ParameterBase : IParameter
	{
		/// <summary>
		/// 题型（标准、随机填空）
		/// </summary>
		public QuestionType QuestionType { get; set; }
		/// <summary>
		/// 在四则运算标准题下指定运算法（加减乘除）
		/// </summary>
		public IList<SignOfOperation> Signs { get; set; }
		/// <summary>
		/// 四则运算类型（标准、随机出题）
		/// </summary>
		public FourOperationsType FourOperationsType { get; set; }
		/// <summary>
		/// 运算结果最大限度值
		/// </summary>
		public int MaximumLimit { get; set; }
		/// <summary>
		/// 出题数量
		/// </summary>
		public int NumberOfQuestions { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public virtual void InitParameter()
		{
		}
	}
}
