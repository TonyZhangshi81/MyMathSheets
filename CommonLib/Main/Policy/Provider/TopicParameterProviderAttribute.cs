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
		///
		/// </summary>
		public TopicParameterProviderAttribute(string name) : base(typeof(TopicParameterProvider))
		{
			Name = name;
		}

		/// <summary>
		/// Provider 名
		/// </summary>
		public string Name
		{
			get;
			set;
		}
	}
}