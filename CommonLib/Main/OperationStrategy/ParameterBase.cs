using MyMathSheets.CommonLib.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 題型參數類
	/// </summary>
	[JsonObject(MemberSerialization.OptOut)]
	public class ParameterBase : IParameter
	{
		/// <summary>
		/// <see cref="ParameterBase"/>的實例
		/// </summary>
		private readonly ParameterHepler helper;

		/// <summary>
		/// 題型參數識別號(topicIdentifier + "::" + identifier)
		/// </summary>
		[JsonProperty(PropertyName = "Identifier")]
		public string TopicArgumentsIdentifier { get; set; }

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
		/// 推算範圍（基於算式第一參數指定範圍的推算）
		/// </summary>
		public List<int> LeftScope { get; set; }

		/// <summary>
		/// 推算範圍（基於算式第二參數指定範圍的推算）
		/// </summary>
		public List<int> RightScope { get; set; }

		/// <summary>
		/// <see cref="ParameterBase"/>的構造
		/// </summary>
		public ParameterBase() => helper = new ParameterHepler();

		/// <summary>
		/// 參數初期化處理
		/// </summary>
		/// <param name="topicArgumentsIdentifier">題型參數識別ID</param>
		internal void InitParameterBase(string topicArgumentsIdentifier)
		{
			// 參數配置json注入parameter
			ParameterBase parameter = helper.CreateParameterProvider().Initialize(topicArgumentsIdentifier);

			QuestionType = parameter.QuestionType;
			Signs = parameter.Signs;
			FourOperationsType = parameter.FourOperationsType;
			MaximumLimit = parameter.MaximumLimit;
			NumberOfQuestions = parameter.NumberOfQuestions;
			Reserve = parameter.Reserve;
			LeftScope = parameter.LeftScope;
			RightScope = parameter.RightScope;
		}

		/// <summary>
		/// 參數初期化處理
		/// </summary>
		public virtual void InitParameter()
		{
		}
	}
}