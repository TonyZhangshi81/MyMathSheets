using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TopicParameterProviderAttribute : ExportAttribute, ITopicParameterProviderMetaDataView
	{
		/// <summary>
		/// <see cref="TopicParameterProviderAttribute"/>的構造函數
		/// </summary>
		public TopicParameterProviderAttribute(string importType) : base(typeof(TopicParameterProvider))
		{
			ImportType = importType;
		}

		/// <summary>
		/// 提供者類型
		/// </summary>
		public string ImportType
		{
			get;
			set;
		}
	}
}