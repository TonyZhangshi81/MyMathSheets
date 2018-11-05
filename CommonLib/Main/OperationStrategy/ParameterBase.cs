using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Provider;
using MyMathSheets.CommonLib.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class ParameterBase : IParameter
	{
		/// <summary>
		/// 識別號
		/// </summary>
		public string Identifier { get; set; }
		/// <summary>
		/// 題型（標準、隨機填空）
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
		/// 參數保留字段
		/// </summary>
		public string Reserve { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual void InitParameter()
		{
			var hepler = new ParameterHepler();
			ParameterBase parameter = hepler.CreateParameterProvider().Initialize(Identifier);

			QuestionType = parameter.QuestionType;
			Signs = parameter.Signs;
			FourOperationsType = parameter.FourOperationsType;
			MaximumLimit = parameter.MaximumLimit;
			NumberOfQuestions = parameter.NumberOfQuestions;
			Reserve = parameter.Reserve;
		}
	}
}
